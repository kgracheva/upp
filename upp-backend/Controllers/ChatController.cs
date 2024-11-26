
using System.Security.Claims;
using System.Xml.Linq;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using upp.Dtos.Chat;
using upp.Dtos.User;
using upp.Services;

namespace upp.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public ChatController(ChatService chatService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ApplicationDbContext context)
    {
        _chatService = chatService;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("open")]
    public async Task<ActionResult<ICollection<MessageDto>>> OpenChat([FromQuery] GetMessagesDto dto)
    {
        return Ok(await _chatService.GetMessages(dto));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ICollection<ChatDto>>> GetChats()
    {
        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        // Console.WriteLine(userIdString);

        return Ok(await _chatService.GetChats(user.Id));
    }


    [HttpPost("simple")]
    public async Task<ActionResult<ChatDto>> CreateSimpleChat([FromBody] CreateSimpleChatDto dto, CancellationToken token)
    {
        try
        {
            var chat = await _chatService.CreateSimpleChat(dto, token);
            return Ok(chat);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }

    }

    [HttpGet("users")]
    public async Task<ActionResult<ICollection<ShortUserDto>>> GetUsers(CancellationToken token)
    {
        var users = await _context.Users.Select(
            x =>
             new ShortUserDto()
             {
                 Id = x.Id,
                 Name = x.Email
             }
            ).ToListAsync();
        return Ok(users);
    }
}

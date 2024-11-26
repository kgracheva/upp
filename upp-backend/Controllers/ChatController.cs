
using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using upp.Dtos.Chat;
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

    public ChatController(ChatService chatService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _chatService = chatService;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    [HttpGet("open")]
    public async Task<ActionResult<ICollection<MessageDto>>> OpenChat([FromQuery] GetMessagesDto dto)
    {
        return Ok(await _chatService.GetMessages(dto));
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
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
        }catch(Exception ex)
        {
            return BadRequest();
        }
        
    }
}

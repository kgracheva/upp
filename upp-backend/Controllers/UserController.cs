using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Training;
using upp.Dtos.User;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(UserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

      
        [HttpGet]
        public async Task<ActionResult<PaginatedList<PhyschologistDto>>> GetArticles([FromQuery] FindUserDto dto, CancellationToken token)
        {
            return Ok(await _userService.Get(dto, token));
        }

        [HttpPost]
        public async Task<ActionResult> ChangeWorkStatus(StatusDto dto, CancellationToken token) {
            await _userService.ChangeWorkStatus(dto.Id, token);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using upp.Dtos.User;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthUserDto>> Login([FromBody] UserLoginDto dto, CancellationToken token)
        {
            return Ok(await _authService.Login(dto, token));
        }

        [HttpPost]
        public async Task<ActionResult<AuthUserDto>> CreateClient([FromBody] AddUserDto dto, CancellationToken token)
        {
            return Ok(await _authService.CreateClient(dto));
        }

        [HttpPost]
        [Route("psy")]
        public async Task<ActionResult<AuthUserDto>> CreatePsy([FromBody] AddUserDto dto, CancellationToken token)
        {
            return Ok(await _authService.CreatePsychologist(dto));
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> CreateAdmin(AddUserDto dto, CancellationToken token)
        //{
        //    return Ok(await _authService.CreateUser(dto, Roles.Admin));
        //}
    }
}

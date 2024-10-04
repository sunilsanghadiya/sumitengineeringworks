using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sew.Models.Dtos;
using sew.Services;

namespace sew.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            Result result = await _userService.LoginUser(loginDto);
            return Ok(result.ApiResult);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto) 
        {
            Result? result = await _userService.Register(registerDto);
            return Ok(result.ApiResult);
        }
    }
}

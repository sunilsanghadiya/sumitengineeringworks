using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sew.Models.Dtos;
using sew.Services;
using static sew.Models.Dtos.OTPDto;

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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            Result result = await _userService.LoginUser(loginDto);
            return Ok(result.ApiResult);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto registerDto) 
        {
            Result? result = await _userService.Register(registerDto);
            return Ok(result.ApiResult);
        }

        [HttpPost("RegisterUserOTP")]
        [AllowAnonymous]
        public Task<IActionResult> RegisterUserOTP(OTPPayload otpPayload) 
        {
            Result? result = _userService.RegisterUserOTP(otpPayload);
            return Task.FromResult<IActionResult>(Ok(result.ApiResult));
        }
    }
}

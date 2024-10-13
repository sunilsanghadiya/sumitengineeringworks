namespace sew.Controllers
{
    
    [Route("api/auth")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) 
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

        [HttpPost("RegisterUserOTP")]        
        public Task<IActionResult> RegisterUserOTP(OTPPayload otpPayload) 
        {
            Result? result = _userService.RegisterUserOTP(otpPayload);
            return Task.FromResult<IActionResult>(Ok(result.ApiResult));
        }
    }
}

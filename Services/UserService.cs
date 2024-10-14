using System.Net;
using authmodule.Common.DTOs;
using authmodule.Helpers;
using AutoMapper;
using Microsoft.Extensions.Options;
using sew.Entities;
using sew.Helpers;
using sew.Helpers.Services;
using sew.Models.Dtos;
using sew.Repository;
using sew.Utility;
using static sew.Models.Dtos.OTPDto;

namespace sew.Services;
public interface IUserService 
{
    Task<Result> LoginUser(LoginDto loginDto);
    Task<Result> Register(RegisterDto registerDto);
    Result RegisterUserOTP(OTPPayload oTPPayload);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public readonly ServiceSettings _serviceSettings;
    private readonly EmailSenderService _emailSenderService;


    public UserService(
        IUserRepository userRepository, 
        IMapper mapper,
        IOptions<ServiceSettings> serviceSettings,
        EmailSenderService emailSenderService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _serviceSettings = serviceSettings.Value;
        _emailSenderService = emailSenderService;
    }

    public async Task<Result> LoginUser(LoginDto loginDto)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(loginDto.Email))
            {
                return new Result($"Please provide email address", loginDto.Email.ToString());
            }
            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return new Result($"Please provide Password address", loginDto.Password.ToString());
            }

            Result? result = new();
            Users? loginUser = new();

            Users? userByEmail = await _userRepository.GetUserByEmailId(loginDto.Email);

            //verify either user auhorized or not
            bool isVerifiedUser = CustomPasswordHasher.VerifyPassword(loginDto.Password, userByEmail.Password);

            if (isVerifiedUser)
            {
                loginDto.Password = userByEmail.Password;
                loginUser = await _userRepository.LoginUser(loginDto);
            }
            else
            {
                return new Result("Unauthorized user.");
            }

            if (loginUser == null)
            {
                return new Result($"An error occurred while getting user : {loginDto.Email.ToString()}", null, HttpStatusCode.InternalServerError);
            }

            LoginResponse? loginResponse = _mapper.Map<LoginResponse>(loginUser);

            #region Generate AccessToken for User
            loginResponse.AccessToken = JwtTokenHelper.GenerateJwtToken(loginResponse?.Email, "sdfs^&&#%GFHeystr6wecewr673674rfhsdvfyu3r46R%E%TSFdsdfsdf");
            #endregion

            result.ResultObject = loginResponse;
            return result;
        }
        catch (Exception ex)
        {
            return new Result($"An error occurred while getting get user list: ", ex.Message);
        }
    }

    public async Task<Result> Register(RegisterDto registerDto)
    {
        Result? result = new();
        try 
        {
            Users? newUser = new();

            #region API VALIDATIONS
            if(string.IsNullOrWhiteSpace(registerDto.Email))
            {
                return new Result($"Please provide email address", registerDto.Email.ToString());
            }
            if(string.IsNullOrWhiteSpace(registerDto.Password))
            {
                return new Result($"Please provide Password", registerDto.Password.ToString());
            }
            if(registerDto.Password.Length < 8)
            {
                return new Result($"Please provide atleast 8 character Password", registerDto.Password.ToString());
            }
            #endregion

            newUser = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                MobileNo = registerDto.MobileNumber,
                Gender = registerDto.Gender,
                IsActive = false,
                IsAdmin = false,
                Created = DateTime.UtcNow,
                IsDeleted = false,
                IsVerifiedUser = false,
                Password = CustomPasswordHasher.HashPassword(registerDto.Password)
            };
            
            #region  OTP Send And Verify
            if(newUser != null) 
            {
                OTPPayload? otpPayload = new()
                {
                    Email = newUser.Email
                };

                result = RegisterUserOTP(otpPayload);
                if(result.HasError)
                {
                    return new Result("An error occurred while RegisterUserOTP");
                }
                
                newUser.OTP = result.ResultObject?.ToString();
            }
            #endregion

            newUser = await _userRepository.Register(newUser);

            if(newUser != null) 
            {
                newUser = await _userRepository.GetUserByID(newUser.ID);
            }

            if(newUser?.OTP == registerDto.OTP) 
            {
                newUser.IsActive = true;
                newUser.IsVerifiedUser = true;
                newUser.OTPExpireDate = DateTime.UtcNow;
                await _userRepository.SaveUser(newUser);
                result.ResultObject = newUser;
            }
            else 
            {
                return new Result("Provided OTP is not valid please try again !");
            }
                
            return result;
        }
        catch(Exception ex)
        {
            return new Result($"An error occurred while register a user: ", ex);
        }
    }

    public Result RegisterUserOTP(OTPPayload oTPPayload)
    {
        Result result = new();
        try
        {
            if (string.IsNullOrWhiteSpace(oTPPayload.Email))
            {
                return new Result($"Please enter emailAddress.", "User.EmailAddressRequired");
            }

            string otp = Utility.Utility.GenderateRandomNo(6);
            string? emailBody = _serviceSettings.OTPEmailTemplate?.Body;
            emailBody = emailBody?.Replace("{{OTP}}", otp);

            EmailDto? emailDto = new()
            {
                ToEmail = oTPPayload.Email,
                EmailSubject = _serviceSettings.OTPEmailTemplate?.Subject ?? "SAMPLE_SUBJECT",
                EmailBody = emailBody ?? string.Empty,
            };

            result = _emailSenderService.SendEmail(emailDto);
            if (result.HasError)
            {
                return new Result($"An error occurred in while SendEmail()");
            }
            return result;
        }
        catch (Exception ex)
        {
            return new Result("An error occurred while registerUserOTP send" + ex.Message, ex);
        }
    }
}

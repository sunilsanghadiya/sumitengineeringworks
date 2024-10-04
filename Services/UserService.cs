using System.Net;
using authmodule.Helpers;
using sew.Entities;
using sew.Models.Dtos;
using sew.Repository;

namespace sew.Services;
public interface IUserService 
{
    Task<Result> LoginUser(LoginDto loginDto);
    Task<Result> Register(RegisterDto registerDto);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> LoginUser(LoginDto loginDto)
    {
        Result? result = new();
        try
        {
            #region API VALIDATIONS
            if(string.IsNullOrWhiteSpace(loginDto.Email))
            {
                return new Result("Email field is required", "User.EmailRequired", HttpStatusCode.BadRequest);
            }
            if(string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return new Result("Password field is required", "User.PasswordRequired", HttpStatusCode.BadRequest);
            }
            #endregion
            
            Users? loginUser = await _userRepository.LoginUser(loginDto);

            result.ResultObject = loginUser;

            return result;
        }
        catch(Exception ex)
        {
            return new Result($"An error occurred while LoginUser() error : {ex.ToString()}", "User.LoginError", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result> Register(RegisterDto registerDto)
        {
            Result? result = new();
            try 
            {
                #region API VALIDATIONS
                if(!registerDto.Email.Contains("@"))
                {
                    return new Result($"Please provide valid email address", registerDto.Email.ToString());
                }
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

                Users? newUser = new()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    MobileNo = registerDto.MobileNumber,
                    Gender = registerDto.Gender,
                    IsActive = true,
                    IsAdmin = false,
                    Created = DateTime.UtcNow,
                    IsDeleted = false,
                    Password = CustomPasswordHasher.HashPassword(registerDto.Password)
                };

                newUser = await _userRepository.Register(newUser);

                result.ResultObject = newUser;
                
                return result;
            }
            catch(Exception ex)
            {
                return new Result($"An error occurred while register a user: ", ex);
            }
        }
}

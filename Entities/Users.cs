namespace sew.Entities;

public class Users
{   
    public int ID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte Gender { get; set; }
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public string? OTP { get; set; }
    public DateTime? OTPExpireDate { get; set; }
    public bool IsVerifiedUser { get; set; }
}

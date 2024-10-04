namespace sew.Models.Dtos;

public class RegisterDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set;} = string.Empty;
    public string MobileNumber { get; set;} = string.Empty;
    public byte Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

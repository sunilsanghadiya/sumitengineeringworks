using System;

namespace sew.Models.Dtos;

public class OTPDto
{
    public class OTPPayload 
    {
        public string Email { get; set; } = string.Empty;  
    }
}

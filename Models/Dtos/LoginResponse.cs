using System;

namespace sew.Models.Dtos;

public class LoginResponse
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public byte Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Password { get; set; } = string.Empty;
    }
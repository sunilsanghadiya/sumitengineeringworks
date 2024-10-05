using System;

namespace sew.Helpers.Services.DTOs;

public class SendEmailDto
{
    public string ToEmail { get; set; } = string.Empty;
    public string EmailSubject { get; set; } = string.Empty;
    public string? CcEmail { get; set; }
    public string? BccEmail { get; set; }
    public string EmaiBody { get; set; } = string.Empty;
    public IFormFileCollection? files { get; set; }
}

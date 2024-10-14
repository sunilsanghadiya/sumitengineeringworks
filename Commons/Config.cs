using System;

namespace sew.Commons;

public class Config
{
    public class ServiceSettings
    {
        public string? AccessToken { get; set; }
        public MailSettings? MailSettings { get; set; }
        public OTPEmailTemplate? OTPEmailTemplate { get; set; }
    }

    public class OTPEmailTemplate
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public class MailSettings 
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUserName  { get; set; } = string.Empty;
        public string SmtpPassword  { get; set; } = string.Empty;
        public string SmtpFromEmailAddress  { get; set; } = string.Empty;
        public string SmtpFromName  { get; set; } = string.Empty;
        public bool IsEnableSsl { get; set; }
        public string DefaultEmailAddress  { get; set; } = string.Empty;
        public string EmailSeparator  { get; set; } = string.Empty;
        public int MaxAllowFileSize { get; set; }
    }




}

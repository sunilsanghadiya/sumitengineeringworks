
namespace authmodule.Common.DTOs
{
    public class EmailDto
    {
        public string ToEmail { get; set; } = string.Empty;
        public List<string> ToEmails { get; set; } = new();
        public List<string>? CcEmails { get; set; }
        public List<string>? BccEmails { get; set; }
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
        public int typeID  { get; set; }    
    }
}
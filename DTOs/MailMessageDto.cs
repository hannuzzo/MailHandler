using System.Collections.Generic;

namespace BatchMail.Mail.DTOs
{
    public class MailMessageDto
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> CC { get; set; }
        public IEnumerable<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Attachments { get; set; }
    }
}

using BatchMail.Mail.DTOs;

namespace BatchMail.Mail.Util
{
    internal static class MailHelper
    {
        public static System.Net.Mail.MailMessage ToNetMailMessage(this MailMessageDto mail)
        {
            var mailMessage = new System.Net.Mail.MailMessage
            {
                From = new System.Net.Mail.MailAddress(mail.From),
                Subject = mail.Subject,
                Body = mail.Body,
                IsBodyHtml = true
            };

            foreach (var to in mail.To)
            {
                mailMessage.To.Add(to);
            }

            foreach (var cc in mail.CC)
            {
                mailMessage.CC.Add(cc);
            }

            foreach (var bcc in mail.BCC)
            {
                mailMessage.Bcc.Add(bcc);
            }

            foreach (var attachment in mail.Attachments)
            {
                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(attachment));
            }

            return mailMessage;
        }

        public static System.Web.Mail.MailMessage ToWebMailMessage(this MailMessageDto mail)
        {

        }
    }
}

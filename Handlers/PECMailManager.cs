namespace BulkMail.Mail
{
    using BatchMail.Mail.Abstracts;
    using BatchMail.Mail.DTOs;
    using BatchMail.Mail.Interfaces;
    using System;
    using System.Linq;

    public class PECMailManager : MailHandlerBase
    {
        private const string SMTP_SERVER = "http://schemas.microsoft.com/cdo/configuration/smtpserver";
        private const string SMTP_SERVER_PORT = "http://schemas.microsoft.com/cdo/configuration/smtpserverport";
        private const string SEND_USING = "http://schemas.microsoft.com/cdo/configuration/sendusing";
        private const string SMTP_USE_SSL = "http://schemas.microsoft.com/cdo/configuration/smtpusessl";
        private const string SMTP_AUTHENTICATE = "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate";
        private const string SEND_USERNAME = "http://schemas.microsoft.com/cdo/configuration/sendusername";
        private const string SEND_PASSWORD = "http://schemas.microsoft.com/cdo/configuration/sendpassword";

        public System.Web.Mail.MailMessage pecMail;

        public PECMailManager(ISmtpConfiguration smtpConfiguration)
            : base(smtpConfiguration)
        {
        }

        private System.Web.Mail.MailMessage CreatePecMail()
        {
            pecMail = new System.Web.Mail.MailMessage();
            pecMail.Fields[SMTP_SERVER] = configuration.Host;
            pecMail.Fields[SMTP_SERVER_PORT] = configuration.Port;
            pecMail.Fields[SEND_USING] = 2;
            pecMail.Fields[SMTP_USE_SSL] = configuration.EnableSsl;
            pecMail.Fields[SMTP_AUTHENTICATE] = 1;
            pecMail.Fields[SEND_USERNAME] = configuration.Username;
            pecMail.Fields[SEND_PASSWORD] = configuration.Password;
        }

        private void SetPecMail(MailMessageDto mail)
        {
            pecMail.From = mail.From;

            if (mail.To != null || mail.To.Count() == 0) 
                throw new ApplicationException("Missing 'To' addresses");
            pecMail.To = string.Join(",", mail.To);

            if (mail.CC != null && mail.CC.Count() != 0)
                pecMail.Cc = string.Join(",", mail.CC);

            if (mail.BCC != null && mail.BCC.Count() != 0)
                pecMail.Bcc = string.Join(",", mail.BCC);

            pecMail.Subject = mail.Body;
            pecMail.Body = mail.Body;

            if (mail.Attachments != null && mail.Attachments.Count() != 0)
            {
                pecMail.Attachments.Clear();
                foreach (var attachment in mail.Attachments)
                    pecMail.Attachments.Add(new System.Web.Mail.MailAttachment(attachment));
            }
        }

        public override void Send(MailMessageDto mail)
        {
            pecMail = CreatePecMail();
            SetPecMail(mail);
            System.Web.Mail.SmtpMail.Send(pecMail);
        }

        public override void Dispose()
        {
            pecMail = null;
        }
    }
}
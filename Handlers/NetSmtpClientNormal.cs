namespace BulkMail.Mail
{
    using BatchMail.Mail.Abstracts;
    using BatchMail.Mail.DTOs;
    using BatchMail.Mail.Interfaces;
    using BatchMail.Mail.Util;
    using System.Net;
    using System.Net.Mail;

    internal class NetSmtpClientNormal : MailHandlerBase
    {
        private SmtpClient smtp;

        public NetSmtpClientNormal(ISmtpConfiguration smtpConfiguration)
            : base(smtpConfiguration)
        {
        }

        private void PrepareSmtpClient()
        {
            if (smtp != null)
                return;

            smtp = new SmtpClient();
            smtp.Host = configuration.Host;
            smtp.Port = configuration.Port;
            smtp.EnableSsl = configuration.EnableSsl;

            if (!string.IsNullOrWhiteSpace(configuration.Username))
                smtp.Credentials = new NetworkCredential(
                    configuration.Username,
                    configuration.Password);
        }

        public override void Send(MailMessageDto mail)
        {
            PrepareSmtpClient();

            using (var netMail = mail.ToNetMailMessage())
                smtp.Send(netMail);            
        }

        public override void Dispose()
        {
            try
            {
                smtp?.Dispose();
            }
            catch
            {
            }
        }
    }

}
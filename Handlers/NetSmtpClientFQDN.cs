namespace BulkMail.Mail
{
    using BatchMail.Mail.Abstracts;
    using BatchMail.Mail.DTOs;
    using BatchMail.Mail.Interfaces;
    using BatchMail.Mail.Util;

    internal class NetSmtpClientFQDN : MailHandlerBase
    {
        private SmtpClientEx smtp;

        public NetSmtpClientFQDN(ISmtpConfiguration smtpConfiguration)
            : base(smtpConfiguration)
        {
        }

        private void PrepareSmtClient()
        {
            if (smtp != null)
                return;

            var user = "";
            var pwd = "";

            if (!string.IsNullOrWhiteSpace(configuration.Username))
            {
                user = configuration.Username;
                pwd = configuration.Password;
            }

            smtp.SetSMTP(configuration.Host, configuration.Port, user, pwd, configuration.EnableSsl);
        }

        public override void Send(MailMessageDto mail)
        {
            PrepareSmtClient();

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
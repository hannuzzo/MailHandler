namespace BatchMail.Mail
{
    using BatchMail.Mail.Abstracts;
    using BatchMail.Mail.Interfaces;
    using System;

    public class MailHandler
    {
        public static MailHandlerBase CreateMailHandler<THandler>(ISmtpConfiguration smtpConfiguration) where THandler : MailHandlerBase
        {
            return (MailHandlerBase)Activator.CreateInstance(typeof(THandler), smtpConfiguration);
        }
    }
}

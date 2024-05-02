using BatchMail.Mail.DTOs;
using BatchMail.Mail.Interfaces;
using BulkMail.Mail.Interfaces;

namespace BatchMail.Mail.Abstracts
{
    public abstract class MailHandlerBase : IMailHandler
    {
        protected readonly ISmtpConfiguration configuration;

        public MailHandlerBase(ISmtpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public virtual void Dispose() { }

        public abstract void Send(MailMessageDto mail);
    }
}
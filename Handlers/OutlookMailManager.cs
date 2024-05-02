namespace BulkMail.Mail
{
    using BatchMail.Mail.Abstracts;
    using BatchMail.Mail.DTOs;
    using BatchMail.Mail.Interfaces;
    using System;

    internal class OutlookMailManager : MailHandlerBase
    {
        public OutlookMailManager(ISmtpConfiguration smtpConfiguration)
            : base(smtpConfiguration)
        {
        }

        public override void Send(MailMessageDto mail)
        {
            throw new NotImplementedException();
        }
    }
}
namespace BulkMail.Mail.Interfaces
{
    using BatchMail.Mail.DTOs;
    using System;

    public interface IMailHandler : IDisposable
    {
        void Send(MailMessageDto mail);
    }
}
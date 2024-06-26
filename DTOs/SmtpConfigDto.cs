﻿namespace BatchMail.Mail.DTOs
{
    using BatchMail.Mail.Interfaces;

    public class SmtpConfigurationDto : ISmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
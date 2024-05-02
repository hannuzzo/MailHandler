namespace BatchMail.Mail.Interfaces
{
    public interface ISmtpConfiguration
    {
        string Host { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool EnableSsl { get; set; }
    }
}

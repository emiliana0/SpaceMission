namespace SPACE.Services;

public interface IEmailService
{
    void SendEmail(
        string senderEmail,
        string appPassword,
        string receiverEmail,
        string subject,
        string body);
}
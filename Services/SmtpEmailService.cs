using System.Net;
using System.Net.Mail;

namespace SPACE.Services;

public class SmtpEmailService : IEmailService
{
    public void SendEmail(
        string senderEmail,
        string appPassword,
        string receiverEmail,
        string subject,
        string body)
    {
        MailMessage message = new();

        message.From = new MailAddress(senderEmail);

        message.To.Add(receiverEmail);

        message.Subject = subject;

        message.Body = body;

        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

        client.Credentials = new NetworkCredential(senderEmail, appPassword);

        client.EnableSsl = true;

        client.Send(message);
    }
}
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using weegci.models;
using weegci.services.interfaces;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace weegci.services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SmtpSettings _smtp;

        public EmailService(IConfiguration config, IOptions<SmtpSettings> smtp)
        {
            _config = config;
            _smtp = smtp.Value;
        }

        public async Task<MailResponseModel> SendEmailAsync(ContactFormViewModel model)
        {
            try
            {
                SmtpSettings settings = _config.GetSection("SmtpSettings").Get<SmtpSettings>();

                // The email that will send the message (SMTP account)
                var smtpFrom = new MailAddress(settings.Username, model.Name);

                // The fixed email that receives messages
                var smtpTo = new MailAddress(settings.ReceiverEmail);

                var body = $@"
                            A new contact message was submitted:

                            Name: {model.Name}
                            Email: {model.Email}

                            Message:
                            {model.Message}
                            ";

                using (var message = new MailMessage(smtpFrom, smtpTo))
                {
                    message.Subject = model.Subject;
                    message.Body = body;

                    var client = new SmtpClient
                    {
                        Host = settings.Host,
                        Port = settings.Port,
                        EnableSsl = settings.EnableSSL,
                        Credentials = new NetworkCredential(settings.Username, settings.Password)
                    };
                    client.UseDefaultCredentials = false;

                    client.Send(message);
                    return new MailResponseModel()
                    {
                        IsSucceeded = true,
                        Message = "Message sent successfully.",
                        Status = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return new MailResponseModel()
                {
                    IsSucceeded = false,
                    Message = $"Error sending message: {ex.Message}",
                    Status = "Error"
                };
            }
        }

    }
}

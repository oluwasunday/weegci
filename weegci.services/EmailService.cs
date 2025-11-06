using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using weegci.models;
using weegci.services.interfaces;
using Microsoft.Extensions.Options;
using System.Runtime;
using Microsoft.Extensions.Configuration.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

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
                var config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();

                var apiKey = config["BrevoSettings:ApiKey"] ?? Environment.GetEnvironmentVariable("BrevoSettings__ApiKey");
                var sender = config["BrevoSettings:RecipientEmail"] ?? "sundayoladejo13@gmail.com";

                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("BREVO_API_KEY not found or empty.");
                }

                // Configure Brevo client
                Configuration.Default.ApiKey.Clear();
                Configuration.Default.ApiKey.Add("api-key", apiKey);
                var apiInstance = new TransactionalEmailsApi();

                // Compose the email
                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender("WEEGCI", sender),
                    to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo("weegci@yahoo.com"), new SendSmtpEmailTo("dominionkoncept01@gmail.com") },
                    subject: model.Subject,
                    htmlContent: $"<p>New message from: <strong>{model.Name} - {model.Email}</strong></p><p>{model.Message}</p>"
                );

                // Send the email
                apiInstance.SendTransacEmail(sendSmtpEmail);

                return new MailResponseModel()
                {
                    IsSucceeded = true,
                    Message = $"Message sent",
                    Status = "Success"
                };
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

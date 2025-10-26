using weegci.models;

namespace weegci.services.interfaces
{
    public interface IEmailService
    {
        Task<MailResponseModel> SendEmailAsync(ContactFormViewModel model);
    }
}

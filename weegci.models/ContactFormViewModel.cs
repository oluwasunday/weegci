using System.ComponentModel.DataAnnotations;

namespace weegci.models
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } // Sender’s email

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}

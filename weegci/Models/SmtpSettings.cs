namespace weegci.Models
{
    public class SmtpSettings
    {
        public string ReceiverEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}

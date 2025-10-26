namespace weegci.models
{
    public class SmtpSettings
    {
        public string ReceiverEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }

        /*
         * 
         * "ReceiverEmail": "sample@gmail.com",
        "Host": "smtp.gmail.com",
        "Port": 587,
        "Username": "abimbolaoladejo25@gmail.com",
        "Password": "19Abimbola@88",
        "EnableSSL": true
         */
    }
}

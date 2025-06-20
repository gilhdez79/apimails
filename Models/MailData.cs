namespace ApiMails.Models
{
    public class MailData
    {
        public IFormFile File { get; set; }
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string NameFile { get; set; }
    }
}

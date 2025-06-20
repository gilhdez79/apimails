namespace ApiMails.Models
{
    public class EmailModel
    {

        public EmailModel()
        {

        }
        private List<String> _To = new List<string>();

        public List<String> To
        {
            get { return _To; }
            set { _To = value; }
        }

        private List<string> _CC = new List<string>();

        public List<string> Cc
        {
            get { return _CC; }
            set { _CC = value; }
        }

        private List<string> _Bcc = new List<string>();

        public List<string> Bcc
        {
            get { return _Bcc; }
            set { _Bcc = value; }
        }




        public String From { get; set; }
        public String User { get; set; }

        public String Body { get; set; }
        public String Subject { get; set; }
        public int Port { get; set; }
        public String Password { get; set; }
        public String Host { get; set; }


        private List<string> _Attachments;

        public List<string> Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }

    }
}

using ApiMails.Models;

namespace ApiMails.interfaces
{
    public interface IMailService
    {

        public Task<bool> SendMailAsync(MailData mailData);
        public bool SendMailV1(EmailModel mailData);
    }
}

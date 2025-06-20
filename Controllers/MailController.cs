using ApiMails.interfaces;
using ApiMails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace ApiMails.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public MailController(IMailService _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMail")]
        public IActionResult SendMail([FromForm] MailData mailData)
        {
            var x = _mailService.SendMailAsync(mailData);
            return Ok(x);
        }

        [HttpPost]
        [Route("SendMailV1")]
        public IActionResult SendMailV1(EmailModel mailData)
        {
            var x = _mailService.SendMailV1(mailData);
            return Ok(x);
        }
    }
}

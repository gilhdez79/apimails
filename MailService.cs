using ApiMails.interfaces;
using ApiMails.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System;
using System.Text;
using MimeKit;
using SmtpClientv1 = System.Net.Mail.SmtpClient;
using System.Runtime.Versioning;
using MailKit.Security;
using System.Web;

namespace ApiMails
{
    public class MailService:IMailService
    {
        private readonly IConfiguration _mailSettings;

        public MailService(IConfiguration mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions;
        }

        public async Task<bool> SendMailAsync(MailData mailData)
        {
            NotificationMetadata _Mail = new NotificationMetadata();

            var connectionString = _mailSettings.GetSection("NotificationMetadata:Port").Value;
            _Mail.Server = _mailSettings.GetSection("NotificationMetadata:Server").Value.ToString();
            _Mail.Port = Convert.ToInt32(_mailSettings.GetSection("NotificationMetadata:Port").Value);
            _Mail.SenderEmail = _mailSettings.GetSection("NotificationMetadata:SenderEmail").Value;
            _Mail.Password = _mailSettings.GetSection("NotificationMetadata:Password").Value;
            _Mail.SenderName = _mailSettings.GetSection("NotificationMetadata:SenderName").Value;
            _Mail.UserName = _mailSettings.GetSection("NotificationMetadata:UserName").Value;
           var strMailCC = _mailSettings.GetSection("DatosMail:MailCC").Value;
            try
            {
                using var memoryStream = new MemoryStream();
                await mailData.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var attachment = new Attachment(memoryStream, mailData.File.FileName, mailData.File.ContentType);



                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_Mail.SenderName, _Mail.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);
                    emailMessage.Bcc.Add(new MailboxAddress(mailData.EmailToName, strMailCC));
                    emailMessage.Subject = mailData.EmailSubject;
                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.HtmlBody = HttpUtility.HtmlDecode(mailData.EmailBody);
                    emailBodyBuilder.Attachments.Add(mailData.File.FileName, memoryStream,MimeKit.ContentType.Parse("image/jpg"));
                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (MailKit.Net.Smtp.SmtpClient mailClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        mailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        mailClient.Connect("mail.cardenas.gob.mx", 587, SecureSocketOptions.Auto);
                        mailClient.Timeout   = 200000;
                        mailClient.Authenticate(_Mail.UserName, _Mail.Password);

                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool SendMailV1(EmailModel _Mail)
        {

            /* var connectionString = configuration.GetSection("MailSettins:Port").Value;
             _Mail.Host = configuration.GetSection("MailSettins:Host").Value.ToString();
             _Mail.Port = Convert.ToInt32(configuration.GetSection("MailSettins:Port").Value);
             _Mail.User = configuration.GetSection("MailSettins:User").Value;
             _Mail.Password = configuration.GetSection("MailSettins:Password").Value;

             */

            MailMessage message = new MailMessage();
            SmtpClientv1 service = new SmtpClientv1();
            String path = String.Empty;

            service.Host =_Mail.Host;
            service.Port = _Mail.Port;
            service.EnableSsl = true;
            service.DeliveryMethod = SmtpDeliveryMethod.Network;
            service.UseDefaultCredentials = false;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_Mail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);

            if (!String.IsNullOrEmpty(_Mail.User))
                service.Credentials = new NetworkCredential(_Mail.User, _Mail.Password);

            if (_Mail.To != null && _Mail.To.Count > 0)
                _Mail.To.ForEach(m => message.To.Add(m));

            if (_Mail.Cc != null && _Mail.Cc.Count > 0)
                _Mail.Cc.ForEach(m => message.CC.Add(m));

            if (_Mail.Cc != null && _Mail.Cc.Count > 0)
                _Mail.Cc.ForEach(m => _Mail.Cc.Add(m));

            if (_Mail.Bcc != null && _Mail.Bcc.Count > 0)
                _Mail.Bcc.ForEach(m => message.Bcc.Add(m));

            if (_Mail.Attachments != null && _Mail.Attachments.Count > 0)
            {
                _Mail.Attachments.ForEach(a =>
                {
                    message.Attachments.Add(new Attachment(a));
                });
            }

            message.Priority = MailPriority.High;
            if (!string.IsNullOrEmpty(_Mail.Subject))
                message.Subject = _Mail.Subject;
            else
                message.Subject = _Mail.Subject;

            message.Body = _Mail.Body;
            message.IsBodyHtml = true;

            //htmlView.LinkedResources.Add(pathImg);
            message.AlternateViews.Add(htmlView);

            message.From = new MailAddress(_Mail.From);

            try
            {
                // service.Send(message);
                // service.SendMailAsync(message);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);

                var smtp = new SmtpClientv1
                {
                    Host = "mail.cardenas.gob.mx",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("sistemasdeinformacion@cardenas.gob.mx", "Sistemas2124$$")
                };

                //  smtp.SendMailAsync(new MailMessage { From = new MailAddress("sistemasdeinformacion@cardenas.gob.mx", "Gilberto"), To = { "gilberto.hdez79@gmail.com" }, Subject = "Generación de Hiupervinculos/Actualizacion Transparencia(WEB)", Body = "Esto es un ejemplo", BodyEncoding = Encoding.UTF8 }).Wait();
                smtp.Send(message);
                // erroremail.Text = "Email has been sent successfully.";
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());

                return false;
            }
        }

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

    }
}

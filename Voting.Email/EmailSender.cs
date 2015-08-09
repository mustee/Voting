using System;
using System.Threading.Tasks;
using Voting.Email.Models;
using System.Configuration;
using System.Reflection;
using System.Net.Mail;
using System.Net;

namespace Voting.Email
{
    public class EmailSender:IEmailSender
    {
        private readonly string host;
        private readonly int port;
        private readonly bool enableSsl;
        private readonly string username;
        private readonly string password;
        private readonly string from;

        public EmailSender()
        {
            host = ConfigurationManager.AppSettings["Email.Host"];
            port = int.Parse(ConfigurationManager.AppSettings["Email.Port"]);
            enableSsl = bool.Parse(ConfigurationManager.AppSettings["Email.EnableSSL"]);
            username = ConfigurationManager.AppSettings["Email.Username"];
            password = ConfigurationManager.AppSettings["Email.Password"];
            from = ConfigurationManager.AppSettings["Email.From"];
        }
        
           
        public Task Send(MailBase model)
        {

            var message = model.Message();

            return Task.Factory.StartNew(() =>
            {
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials =
                        new NetworkCredential(username, password)
                };

                var fromAddress = new MailAddress(from, string.Empty);
                var toAddress = new MailAddress(model.To, string.Empty);

                var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = model.Subject,
                    Body = message,
                    IsBodyHtml = true
                };

                smtp.Send(mailMessage);
            });
        }
    }
}


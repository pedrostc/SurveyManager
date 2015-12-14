using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SurveyManager.Services.Notifier.contract;

namespace SurveyManager.Services.Notifier.Notifiers
{
    public abstract class EmailNotifier : INotifier
    {
        public abstract string SmtpAddress { get; }
        public abstract int SmtpPort { get; }
        public abstract bool UseSSL { get; }
        public abstract string SmtpUser { get; }
        public abstract string SmtpPassword { get; }

        protected SmtpClient Client { get; set; }

        protected EmailNotifier()
        {
            SetupSmtpClient();
        }

        private void SetupSmtpClient()
        {
            Client = new SmtpClient(SmtpAddress, SmtpPort)
            {
                EnableSsl = UseSSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SmtpUser, SmtpPassword)
            };
        }

        private MailMessage SetupMessage(string subject, string body, List<string> destinations, bool htmlBody)
        {
            MailMessage mail = new MailMessage()
            {
                Body = body,
                IsBodyHtml = htmlBody,
                Subject = subject,
                From = new MailAddress("pedrost.rj@gmail.com")
            };

            destinations.ForEach(d => mail.To.Add(d));

            return mail;
        }

        public void Send(string subject, string body, List<string> destinations)
        {
            MailMessage mail = SetupMessage(subject, body, destinations, false);
            SendMail(mail);
        }

        public void SendHtml(string subject, string body, List<string> destinations)
        {
            MailMessage mail = SetupMessage(subject, body, destinations, true);
            SendMail(mail);
        }

        private void SendMail(MailMessage mail)
        {
            Client.Send(mail);
        }
    }
}

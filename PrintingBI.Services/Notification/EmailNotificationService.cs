using Microsoft.Extensions.Logging;
using PrintingBI.Common;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrintingBI.Services.Notification
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailConfig _emailConfig;
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(IEmailConfig emailConfig, 
                                        ILogger<EmailNotificationService> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }

        public bool SendEmail(string to, string subject, string bodyTemplate, bool isHtml = false, string bcc = "", string ccMail = "", string attachmentFileName = "")
        {
            var email = _emailConfig.Email;
            var password = _emailConfig.Password;
            var portNumber = _emailConfig.PortNumber;
            var hostName = _emailConfig.HostName;

            var mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(email);
            mail.Subject = subject;
            mail.Body = bodyTemplate;
            mail.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(ccMail))
                mail.CC.Add(ccMail);

            var smtp = new SmtpClient
            {
                Host = hostName,
                Port = portNumber,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(email, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                smtp.Send(mail);
            }
            catch (Exception)
            {
                _logger.LogError($"Could not send email with subject: {subject} to {email}. The message body is: {bodyTemplate}.");
                return false;
            }

            return true;
        }

        public void SendAsyncEmail(string to, string subject, string body, 
                                          bool isHtml = false, string bcc = "",
                                          string cc = "", string attachmentFileName = "")
        {
            var task = new Task(() =>
                    SendEmail(to, subject, body, isHtml, bcc, cc, attachmentFileName));
            task.Start();
        }
    }
}

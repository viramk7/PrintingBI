namespace PrintingBI.Services.Notification
{
    public interface IEmailNotificationService
    {
        bool SendEmail(string to, string subject, string bodyTemplate, bool isHtml = false, string bcc = "", string ccMail = "", string attachmentFileName = "");
        void SendAsyncEmail(string to, string subject, string bodyTemplate, bool isHtml = false, string bcc = "", string ccMail = "", string attachmentFileName = "");
    }
}

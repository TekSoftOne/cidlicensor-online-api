using System;
using System.Net.Mail;
using System.Net.Mime;

namespace OR.Web
{
    public static class Mailing
    {
        public static EmailModel CreateEmail(string toEmail, string subject, string body)
        {
            return new EmailModel
            {
                Body = body,
                ToEmail = toEmail,
                Subject = subject,
                HasAttachment = false,
                AttachmentPath = "",
                SettingFromEmail = "Online Request <noreply@teksoft1.com>",
                SettingSMTPServer = "smtp.gmail.com",
                SettingSMTPUserName = "josephnguyen@teksoft1.com",
                SettingSMTPPassword = "pyyoppaajylsytel",
                SettingPort = "587",
                SettingSSL = true
            };
        }

        public static bool Send(EmailModel emailModel)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(emailModel.ToEmail, ""));
                // From
                mailMsg.From = new MailAddress(emailModel.SettingFromEmail, "");

                // Subject and multipart/alternative Body
                mailMsg.Subject = emailModel.Subject;
                string text = emailModel.Body;
                string html = emailModel.Body;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                if (emailModel.HasAttachment)
                {
                    mailMsg.Attachments.Add(new Attachment(emailModel.AttachmentPath));
                }

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(emailModel.SettingSMTPServer, Convert.ToInt32(emailModel.SettingPort));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(emailModel.SettingSMTPUserName, emailModel.SettingSMTPPassword);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = emailModel.SettingSSL;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

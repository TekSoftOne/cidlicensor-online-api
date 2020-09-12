using System;
namespace OR.Web
{
    public class EmailModel
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool HasAttachment { get; set; }
        public string AttachmentPath { get; set; }
        public string SettingFromEmail { get; set; }
        public string SettingSMTPServer { get; set; }
        public string SettingSMTPUserName { get; set; }
        public string SettingSMTPPassword { get; set; }
        public string SettingPort { get; set; }
        public bool SettingSSL { get; set; }

    }
}

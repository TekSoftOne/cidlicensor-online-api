using System;
namespace OR.Web.Twilio
{
    public class VerificationModel
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
    }
}

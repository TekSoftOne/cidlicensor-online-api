using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OR.Data;
using OR.Data.ViewModels;

namespace OR.Web.Apis
{
    public class ApplicationSearchModel
    {
        public int ApplicationNumber { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MembershipRequestsController : ControllerBase
    {
        private readonly IDataFactory _dbContext;
        public MembershipRequestsController(IDataFactory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("New")]
        public async Task<IActionResult> CreateRequest([FromBody] MembershipRequestModel request)
        {
            var membershipId = await this._dbContext.MembershipRequests.CreateMembershipRequest(request);
            var applicationId = await this._dbContext.Applications.CreateApplication(membershipId);

            var customerEmail = "tiennsloit@gmail.com";
            var customerName = "Joseph";
            var emailSubject = "Hello from Online Request";
            var emailBody = $"Hello {customerName}, your application number is {applicationId}, please use this to...";

            var email = CreateEmail(customerEmail, emailSubject, emailBody);
            Mailing.Send(email);
            return new OkObjectResult(applicationId);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchRequest([FromBody] ApplicationSearchModel appSearch)
        {
            var request = await _dbContext.MembershipRequests.GetRequest(appSearch.ApplicationNumber);

            return new OkObjectResult(request);
        }

        private EmailModel CreateEmail(string toEmail, string subject, string body)
        {
            return new EmailModel
            {
                Body = body,
                ToEmail = toEmail,
                Subject = subject,
                HasAttachment = false,
                AttachmentPath = "",
                SettingFromEmail = "josephnguyen@teksoft1.com",
                SettingSMTPServer = "smtp.gmail.com",
                SettingSMTPUserName = "josephnguyen@teksoft1.com",
                SettingSMTPPassword = "pyyoppaajylsytel",
                SettingPort = "587",
                SettingSSL = true
            };
        }
    }
}
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OR.CloudStorage;
using OR.Data;
using OR.Data.ViewModels;

namespace OR.Web.Apis
{
    public class ApplicationSearchModel
    {
        public int ApplicationNumber { get; set; }
    }

    public class MembershipUpdateModel
    {
        public string MembershipNumber { get; set; }
        public string Status { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MembershipRequestsController : ControllerBase
    {
        private readonly IDataFactory _dbContext;
        private readonly IRequestStorageManager _requestStorageManager;
        private readonly IMapper _mapper;
        public MembershipRequestsController(IDataFactory dbContext,
            IRequestStorageManager requestStorageManager,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _requestStorageManager = requestStorageManager;
            _mapper = mapper;
        }

        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] MembershipUpdateModel status)
        {
            return new OkObjectResult("Success");
        }


        [HttpPost("New")]
        public async Task<IActionResult> CreateRequest([FromForm] MembershipRequestModel requestModel)
        {
            try
            {
                if (requestModel.PassportAttachement != null)
                {
                    //upload file
                    requestModel.PassportAttachementUrl = await this._requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "passport", requestModel.PassportAttachement);
                }

                if (requestModel.ProfilePhoto != null)
                {
                    //upload file
                    requestModel.ProfilePhotoUrl = await this._requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "profilePhoto", requestModel.ProfilePhoto);
                }

                if (requestModel.AuthorizationLetter != null)
                {
                    //upload file
                    requestModel.AuthorizationLetterUrl = await this._requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "authorizationLetter", requestModel.AuthorizationLetter);
                }

                if (requestModel.EmiratesIdBack != null)
                {
                    //upload file
                    requestModel.EmiratesIdBackUrl = await this._requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "emiratesIdBack", requestModel.EmiratesIdBack);
                }

                if (requestModel.EmiratesIdFront != null)
                {
                    //upload file
                    requestModel.EmiratesIdFrontUrl = await this._requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "emiratesIdFront", requestModel.EmiratesIdFront);
                }

                var membershipId = await _dbContext.MembershipRequests.CreateMembershipRequest(requestModel);
                var applicationId = await _dbContext.Applications.CreateApplication(membershipId);

                var customerEmail = "tiennsloit@gmail.com";
                var customerName = "Joseph";
                var emailSubject = "Hello from Online Request";
                var emailBody = $"Hello {customerName}, your application number is {applicationId}, please use this to...";

                var email = Mailing.CreateEmail(customerEmail, emailSubject, emailBody);
                Mailing.Send(email);
                return new OkObjectResult(applicationId);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchRequest([FromBody] ApplicationSearchModel appSearch)
        {
            try
            {
                var app = await _dbContext.Applications.GetApplication(appSearch.ApplicationNumber);

                var request = await _dbContext.MembershipRequests.GetRequest(app.MembershipId);

                var applicationiRequest = _mapper.Map<MembershipRequestModel>(request);

                applicationiRequest.ApplicationNumber = app.ApplicationNumber;

                return new OkObjectResult(request);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
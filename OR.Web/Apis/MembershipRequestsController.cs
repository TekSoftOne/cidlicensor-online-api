using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OR.CloudStorage;
using OR.Data;
using OR.Data.ViewModels;
using static OR.Data.Constants.Data;

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


        [HttpPost("Update")]
        public async Task<IActionResult> CreateRequest([FromForm] MembershipRequestModel requestModel)
        {

            var isNew = requestModel.ApplicationNumber <= 0;
            try
            {
                if (requestModel.PassportAttachement != null)
                {
                    //upload file
                    requestModel.PassportAttachementUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "passport", requestModel.PassportAttachement);
                }

                if (requestModel.ProfilePhoto != null)
                {
                    //upload file
                    requestModel.ProfilePhotoUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "profilePhoto", requestModel.ProfilePhoto);
                }

                if (requestModel.AuthorizationLetter != null)
                {
                    //upload file
                    requestModel.AuthorizationLetterUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "authorizationLetter", requestModel.AuthorizationLetter);
                }

                if (requestModel.EmiratesIdBack != null)
                {
                    //upload file
                    requestModel.EmiratesIdBackUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "emiratesIdBack", requestModel.EmiratesIdBack);
                }

                if (requestModel.EmiratesIdFront != null)
                {
                    //upload file
                    requestModel.EmiratesIdFrontUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIdNumber, "emiratesIdFront", requestModel.EmiratesIdFront);
                }

                var membership = await _dbContext.MembershipRequests.UpdateMembership(requestModel);
                var applicationId = 0;

                var emailSubject = "";
                var emailBody = "";

                var customerEmail = requestModel.EmailAddress;
                var customerName = requestModel.FullName;

                if (isNew)
                {

                    applicationId = await _dbContext.Applications.CreateApplication(membership.MembershipRequestId);
                    emailSubject = "Hello from Online Request";
                    emailBody = $"Hello {customerName}, your application number is {applicationId}, please use this to...";
                }
                else
                {
                    emailSubject = "Update application success!";
                    emailBody = $"Hello {customerName}, You 've updated successfully information... for {applicationId}!";
                }

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

                var applicationiRequest = _mapper.Map<MembershipRequestResultModel>(request);

                applicationiRequest.ApplicationNumber = app.ApplicationNumber;
                applicationiRequest.TypeOfCustomer = ((TypeOfCustomer)int.Parse(applicationiRequest.TypeOfCustomer)).ToString();
                applicationiRequest.RequestCategory = ((RequestCategory)int.Parse(applicationiRequest.RequestCategory)).ToString();

                return new OkObjectResult(applicationiRequest);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
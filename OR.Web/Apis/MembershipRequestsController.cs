using System;
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
        public string PhoneNumber { get; set; }
    }

    public class MembershipUpdateModel
    {
        public string MembershipNumber { get; set; }
        public int MembershipId { get; set; }
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
        public async Task<IActionResult> UpdateStatus([FromBody] MembershipUpdateModel statusModel)
        {
            try
            {
                var membership = await _dbContext.MembershipRequests.GetMembership(statusModel.MembershipNumber);
                if (membership == null)
                {
                    return BadRequest(new Exception("Can not find this membership!"));
                }

                var app = await _dbContext.Applications.GetApplicationByMembership(membership.MembershipRequestId);

                if (app == null)
                {
                    return BadRequest(new Exception("Can not find application of this membership!"));
                }

                await _dbContext.MembershipRequests.UpdateStatus(statusModel.MembershipNumber, statusModel.Status);

                if (statusModel.Status != "APPROVED_BY_AGENT_WAITING_FOR_ADMIN")
                {
                    var emailSubject = "Application Updates!";
                    var emailBody = $"Hello {membership.FullName}, your application number {app.ApplicationNumber} status was changed to: {statusModel.Status}";

                    var email = Mailing.CreateEmail(membership.Email, emailSubject, emailBody);
                    Mailing.Send(email);
                }


                return new OkObjectResult("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> CreateOrUpdateRequest([FromForm] MembershipRequestModel requestModel)
        {

            var isNew = requestModel.ApplicationNumber <= 0;
            try
            {
                if (requestModel.PassportAttachement != null)
                {
                    //upload file
                    requestModel.PassportAttachementUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIDNumber, "passport", requestModel.PassportAttachement);
                }

                if (requestModel.ProfilePhoto != null)
                {
                    //upload file
                    requestModel.ProfilePhotoUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIDNumber, "profilePhoto", requestModel.ProfilePhoto);
                }

                if (requestModel.AuthorizationLetter != null)
                {
                    //upload file
                    requestModel.AuthorizationLetterUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIDNumber, "authorizationLetter", requestModel.AuthorizationLetter);
                }

                if (requestModel.EmiratesIdBack != null)
                {
                    //upload file
                    requestModel.EmiratesIdBackUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIDNumber, "emiratesIdBack", requestModel.EmiratesIdBack);
                }

                if (requestModel.EmiratesIdFront != null)
                {
                    //upload file
                    requestModel.EmiratesIdFrontUrl = await _requestStorageManager.UploadDocument(requestModel.EmiratesIDNumber, "emiratesIdFront", requestModel.EmiratesIdFront);
                }

                var membership = await _dbContext.MembershipRequests.UpdateMembership(requestModel);
                var applicationId = requestModel.ApplicationNumber;

                var emailSubject = "";
                var emailBody = "";

                var customerEmail = requestModel.Email;
                var customerName = requestModel.FullName;

                if (isNew)
                {
                    requestModel.Status = Status.Pending;
                    applicationId = await _dbContext.Applications.CreateApplication(membership.MembershipRequestId);
                    emailSubject = "Online Request - Application Creation";
                    emailBody = $"Hi {customerName}, your application number is {applicationId}, please use this to...";
                }
                else
                {
                    emailSubject = "Online Request - Application Update";
                    emailBody = $"Hi {customerName}, You 've updated successfully information... for application number {applicationId}!";
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
                if (app == null)
                {
                    return BadRequest(new Exception("Request not found!"));
                }

                var request = await _dbContext.MembershipRequests.GetRequest(app.MembershipId);

                var applicationiRequest = _mapper.Map<MembershipRequestResultModel>(request);

                if (applicationiRequest?.PhoneNumber != appSearch.PhoneNumber)
                {
                    return BadRequest(new Exception("This application number is not belong to the current phone number!"));
                }

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
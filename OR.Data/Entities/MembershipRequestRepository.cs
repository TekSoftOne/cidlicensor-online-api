using System.Threading.Tasks;
using System.Linq;
using OR.Data.ViewModels;
using OR.CloudStorage;

namespace OR.Data
{
    public class MembershipRequestRepository : Repository<MembershipRequest>, IMembershipRequestRepository
    {
        private readonly IRequestStorageManager _requestStorageManager;
        public MembershipRequestRepository(ApplicationDbContext context,
            IRequestStorageManager _requestStorageManager) : base(context)
        { }
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public async Task<int> CreateMembershipRequest(MembershipRequestModel requestModel)
        {
            var membershipRequest = new MembershipRequest
            {
                FullAddress = requestModel.FullAddress,
                Name = requestModel.Name,
                BirthDay = requestModel.BirthDay,
                EmailAddress = requestModel.EmailAddress,
                EmiratesIdNumber = requestModel.EmiratesIdNumber,
                GenderId = (int)requestModel.GenderId,
                LocationId = requestModel.LocationId,
                NationId = requestModel.NationId,
                Occupation = requestModel.Occupation,
                PassportNumber = requestModel.PassportNumber,
                ReligionId = requestModel.ReligionId,
                VisaResidency = requestModel.VisaResidency

            };

            if (requestModel.PassportAttachement != null)
            {
                //upload file
                membershipRequest.PassportAttachement = await this._requestStorageManager.UploadDocument(requestModel.EmailAddress, "passport", requestModel.PassportAttachement);
            }

            if (requestModel.ProfilePhoto != null)
            {
                //upload file
                membershipRequest.ProfilePhoto = await this._requestStorageManager.UploadDocument(requestModel.EmailAddress, "profilePhoto", requestModel.ProfilePhoto);
            }

            if (requestModel.AuthorizationLetter != null)
            {
                //upload file
                membershipRequest.AuthorizationLetter = await this._requestStorageManager.UploadDocument(requestModel.EmailAddress, "authorizationLetter", requestModel.AuthorizationLetter);
            }

            if (requestModel.EmiratesIdBack != null)
            {
                //upload file
                membershipRequest.EmiratesIdBack = await this._requestStorageManager.UploadDocument(requestModel.EmailAddress, "emiratesIdBack", requestModel.EmiratesIdBack);
            }

            if (requestModel.EmiratesIdFront != null)
            {
                //upload file
                membershipRequest.EmiratesIdFront = await this._requestStorageManager.UploadDocument(requestModel.EmailAddress, "emiratesIdFront", requestModel.EmiratesIdFront);
            }


            this.appContext.MembershipRequests.Add(membershipRequest);
            await this.appContext.SaveChangesAsync();
            return membershipRequest.MembershipRequestId;

        }

        public async Task<MembershipRequest> GetRequest(int applicationNumber)
        {
            MembershipRequest m = null;
            var app = await this.appContext.Applications.FindAsync(applicationNumber);
            if (app != null)
            {
                m = await this.appContext.MembershipRequests.FindAsync(app.MembershipId);
            }

            return m;
        }
    }
}

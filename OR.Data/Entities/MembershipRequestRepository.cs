using System.Threading.Tasks;
using System.Linq;
using OR.Data.ViewModels;
using OR.CloudStorage;

namespace OR.Data
{
    public class MembershipRequestRepository : Repository<MembershipRequest>, IMembershipRequestRepository
    {

        public MembershipRequestRepository(ApplicationDbContext context
           ) : base(context)
        {

        }
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public async Task<int> CreateMembershipRequest(MembershipRequestModel requestModel)
        {
            var membershipRequest = new MembershipRequest
            {
                FullAddress = requestModel.FullAddress,
                Name = requestModel.FullName,
                BirthDay = requestModel.BirthDate,
                EmailAddress = requestModel.EmailAddress,
                EmiratesIdNumber = requestModel.EmiratesIdNumber,
                GenderId = (int)requestModel.GenderId,
                LocationId = requestModel.LocationId,
                NationId = requestModel.NationId,
                Occupation = requestModel.Occupation,
                PassportNumber = requestModel.PassportNumber,
                ReligionId = requestModel.ReligionId,
                VisaResidency = requestModel.VisaResidency,
                AuthorizationLetter = requestModel.AuthorizationLetterUrl,
                EmiratesIdBack = requestModel.EmiratesIdBackUrl,
                EmiratesIdFront = requestModel.EmiratesIdFrontUrl,
                PassportAttachement = requestModel.PassportAttachementUrl,
                ProfilePhoto = requestModel.ProfilePhotoUrl,
                RequestCategory = (int)requestModel.RequestCategory,
                PhoneNumber = requestModel.PhoneNumber

            };

            appContext.MembershipRequests.Add(membershipRequest);
            await appContext.SaveChangesAsync();
            return membershipRequest.MembershipRequestId;

        }

        public async Task<MembershipRequest> GetRequest(int applicationNumber)
        {
            MembershipRequest request = null;
            var app = await appContext.Applications.FindAsync(applicationNumber);
            if (app != null)
            {
                request = await appContext.MembershipRequests.FindAsync(app.MembershipId);
            }

            return request;
        }
    }
}

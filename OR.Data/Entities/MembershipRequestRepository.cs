using System.Threading.Tasks;
using System.Linq;
using OR.Data.ViewModels;
using OR.CloudStorage;
using AutoMapper;

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

        public async Task<MembershipRequest> UpdateMembership(MembershipRequestModel requestModel)
        {
            var isNew = requestModel.MembershipRequestId <= 0;
            MembershipRequest membership = new MembershipRequest();
            if (!isNew)
            {
                membership = await appContext.MembershipRequests.FindAsync(requestModel.MembershipRequestId);
            }

            membership.FullAddress = requestModel.FullAddress;
            membership.FullName = requestModel.FullName;
            membership.BirthDate = requestModel.BirthDate;
            membership.Email = requestModel.Email;
            membership.EmiratesIDNumber = requestModel.EmiratesIDNumber;
            membership.GenderId = (int)requestModel.GenderId;
            membership.LocationId = requestModel.LocationId;
            membership.NationId = requestModel.NationId;
            membership.Address = requestModel.Address;
            membership.PassportNumber = requestModel.PassportNumber;
            membership.ReligionId = requestModel.ReligionId;
            membership.VisaResidency = requestModel.VisaResidency;
            membership.AuthorizationLetterUrl = requestModel.AuthorizationLetterUrl;
            membership.EmiratesIdBackUrl = requestModel.EmiratesIdBackUrl;
            membership.EmiratesIdFrontUrl = requestModel.EmiratesIdFrontUrl;
            membership.PassportAttachementUrl = requestModel.PassportAttachementUrl;
            membership.ProfilePhotoUrl = requestModel.ProfilePhotoUrl;
            membership.RequestCategory = (int)requestModel.RequestCategory;
            membership.TypeOfCustomer = (int)requestModel.TypeOfCustomer;
            membership.PhoneNumber = requestModel.PhoneNumber;
            membership.AreaId = requestModel.AreaId;
            membership.AgentId = requestModel.AgentId;
            membership.MembershipId = requestModel.MembershipId;
            membership.MembershipNumber = requestModel.MembershipNumber;

            if (isNew)
            {
                appContext.MembershipRequests.Add(membership);

            }
            else
            {
                appContext.MembershipRequests.Update(membership);
            }

            await appContext.SaveChangesAsync();


            return membership;
        }

        public async Task<MembershipRequest> GetRequest(int membershipId)
        {
            return await appContext.MembershipRequests.FindAsync(membershipId);
        }
    }
}

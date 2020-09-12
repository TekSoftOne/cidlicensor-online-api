using System;
using System.Threading.Tasks;
using OR.Data.ViewModels;

namespace OR.Data
{
    public interface IMembershipRequestRepository : IRepository<MembershipRequest>
    {
        Task<int> CreateMembershipRequest(MembershipRequestModel requestModel);
        Task<MembershipRequest> GetRequest(int applicationNumber);
    }
}

using System;
using System.Threading.Tasks;

namespace OR.Data
{
    public interface IMembershipRequestRepository : IRepository<MembershipRequest>
    {
        Task<int> CreateMembershipRequest();
        Task<MembershipRequest> GetRequest(int applicationNumber);
    }
}

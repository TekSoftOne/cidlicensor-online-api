using System;
using System.Threading.Tasks;

namespace OR.Data
{
    public interface IMembershipRequestRepository : IRepository<MembershipRequest>
    {
        Task CreateMembershipRequest();
    }
}

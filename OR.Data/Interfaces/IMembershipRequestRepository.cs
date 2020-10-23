using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OR.Data.ViewModels;

namespace OR.Data
{
    public interface IMembershipRequestRepository : IRepository<MembershipRequest>
    {
        Task<MembershipRequest> UpdateMembership(MembershipRequestModel requestModel);
        Task<MembershipRequest> GetRequest(int applicationNumber);
        Task<IEnumerable<MembershipRequest>> GetMemberships(string membershipNumber);
        Task<bool> UpdateStatus(string membershipNumber, string status);
        Task<MembershipRequest> GetMembership(string membershipNumber);
        Task<IEnumerable<MembershipRequest>> GetMembershipOfUser(string phoneNumber);
    }
}

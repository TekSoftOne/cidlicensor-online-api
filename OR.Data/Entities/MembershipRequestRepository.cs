using System.Threading.Tasks;

namespace OR.Data
{
    public class MembershipRequestRepository : Repository<MembershipRequest>, IMembershipRequestRepository
    {
        public MembershipRequestRepository(ApplicationDbContext context) : base(context)
        { }
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public async Task<int> CreateMembershipRequest()
        {
            var membershipRequest = new MembershipRequest { FullAddress = "address1" };
            this.appContext.MembershipRequests.Add(membershipRequest);
            await this.appContext.SaveChangesAsync();
            return membershipRequest.MembershipRequestId;

        }
    }
}

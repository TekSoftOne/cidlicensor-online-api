using System.Threading.Tasks;
using System.Linq;
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

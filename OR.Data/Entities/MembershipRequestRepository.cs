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
            this.appContext.MembershipRequests.Add(new MembershipRequest { FullAddress = "address1" });
            return await this.appContext.SaveChangesAsync();

        }
    }
}

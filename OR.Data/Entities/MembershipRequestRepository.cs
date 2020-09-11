using System;
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

        public async Task CreateMembershipRequest()
        {
            this.appContext.MembershipRequests.Add(new MembershipRequest { ApplicationNumber = "123213123", FullAddress = "address1" });
            await this.appContext.SaveChangesAsync();

        }
    }
}

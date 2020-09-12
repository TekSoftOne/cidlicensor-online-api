using System;
using System.Threading.Tasks;
using OR.Data.Interfaces;

namespace OR.Data.Entities
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public async Task<int> CreateApplication(int membershipId)
        {
            appContext.Add(new Application { MembershipId = membershipId });
            return await appContext.SaveChangesAsync();
        }
    }


}

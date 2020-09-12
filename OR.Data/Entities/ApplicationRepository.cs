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
            var app = new Application { MembershipId = membershipId };
            appContext.Add(app);
            await appContext.SaveChangesAsync();
            return app.ApplicationNumber;
        }
    }


}

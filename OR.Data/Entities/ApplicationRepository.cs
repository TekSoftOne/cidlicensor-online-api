using System;
using System.Threading.Tasks;
using OR.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Application> GetApplication(int applicationNumber)
        {
            return await appContext.Applications.FindAsync(applicationNumber);
        }

        public async Task<Application> GetApplicationByMembership(int membershipId)
        {
            return await appContext.Applications.Where(a => a.MembershipId == membershipId).FirstOrDefaultAsync();
        }
    }


}

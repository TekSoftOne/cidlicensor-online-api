using System;
using System.Threading.Tasks;

namespace OR.Data.Interfaces
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Task<int> CreateApplication(int membershipId);
        Task<Application> GetApplication(int applicationNumber);
        Task<Application> GetApplicationByMembership(int membershipId);
    }
}

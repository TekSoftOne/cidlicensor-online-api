using System;
using System.Threading.Tasks;

namespace OR.Data.Interfaces
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Task<int> CreateApplication(int membershipId);
    }
}

using OR.Data.Entities;
using OR.Data.Interfaces;

namespace OR.Data
{
    public class DataFactory : IDataFactory
    {
        readonly ApplicationDbContext _context;

        public DataFactory(ApplicationDbContext context)
        {
            _context = context;
        }


        MembershipRequestRepository _membershipRequestRepository;
        ApplicationRepository _applicationRepository;

        public IMembershipRequestRepository MembershipRequests
        {
            get
            {
                if (_membershipRequestRepository == null)
                    _membershipRequestRepository = new MembershipRequestRepository(_context);
                return _membershipRequestRepository;
            }
        }

        public IApplicationRepository Applications
        {
            get
            {
                if (_applicationRepository == null)
                    _applicationRepository = new ApplicationRepository(_context);
                return _applicationRepository;
            }
        }



        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
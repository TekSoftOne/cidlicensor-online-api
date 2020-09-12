using OR.Data;

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

        public IMembershipRequestRepository MembershipRequests
        {
            get
            {
                if (_membershipRequestRepository == null)
                    _membershipRequestRepository = new MembershipRequestRepository(_context);
                return _membershipRequestRepository;
            }
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
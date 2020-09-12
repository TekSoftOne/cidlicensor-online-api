using OR.Data.Interfaces;

namespace OR.Data
{
    public interface IDataFactory
    {

        IMembershipRequestRepository MembershipRequests { get; }
        IApplicationRepository Applications { get; }
    }
}
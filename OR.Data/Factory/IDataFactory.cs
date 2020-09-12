using OR.Data;

namespace OR.Data
{
    public interface IDataFactory
    {

        IMembershipRequestRepository MembershipRequests { get; }
    }
}
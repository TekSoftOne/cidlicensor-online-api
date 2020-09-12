using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OR.Data;

namespace OR.Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipRequestsController : ControllerBase
    {
        private readonly IDataFactory _dbContext;
        public MembershipRequestsController(IDataFactory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("New")]
        public async Task<IActionResult> CreateRequest()
        {
            var membershipId = await this._dbContext.MembershipRequests.CreateMembershipRequest();
            var applicationId = await this._dbContext.Applications.CreateApplication(membershipId);
            return new OkObjectResult(applicationId);
        }
    }
}
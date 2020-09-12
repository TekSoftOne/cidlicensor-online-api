using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            this._dbContext = dbContext;
        }
        [HttpPost("New")]
        public async Task<IActionResult> CreateRequest()
        {
            await this._dbContext.MembershipRequests.CreateMembershipRequest();
            return new OkResult();
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OR.Web.Twilio;

namespace OR.Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonController : ControllerBase
    {
        private readonly IVerification _verification;
        public CommonController()
        {

        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            return new OkResult();
        }
    }
}

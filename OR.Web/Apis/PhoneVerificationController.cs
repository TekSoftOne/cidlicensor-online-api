using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OR.Web.Twilio;

namespace OR.Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneVerificationController : ControllerBase
    {
        private readonly IVerification _verification;
        public PhoneVerificationController(IVerification verification)
        {
            this._verification = verification;
        }

        [HttpPost("check")]
        public async Task<VerificationResult> Check([FromBody] VerificationModel verificationModel)
        {
            return await _verification.StartVerificationAsync(verificationModel.PhoneNumber, "sms");
        }

        [HttpPost("checkCode")]
        public async Task<VerificationResult> CheckCode([FromBody] VerificationModel verificationCode)
        {
            return await _verification.CheckVerificationAsync(verificationCode.PhoneNumber, verificationCode.Code);
        }
    }
}

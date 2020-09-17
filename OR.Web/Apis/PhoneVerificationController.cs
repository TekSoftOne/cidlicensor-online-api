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
        public async Task<VerificationResult> Check([FromBody] string phoneNumber)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            //if (!user.Verified)
            //{
            return await _verification.StartVerificationAsync(phoneNumber, "sms");
            //}

            //return new VerificationResult(new List<string>{"Your phone number is already verified"});
        }

        [HttpPost("checkCode")]
        public async Task<VerificationResult> CheckCode([FromBody] VerificationModel verificationCode)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            //if (!user.Verified)
            //{
            return await _verification.CheckVerificationAsync(verificationCode.PhoneNumber, verificationCode.Code);
            //}

            //return new VerificationResult(new List<string>{"Your phone number is already verified"});
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OR.Web
{

    public class UserModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetUsers()
        {
            var userNames = _userManager.Users.Select(u => u.UserName);
            return new OkObjectResult(userNames);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<IdentityResult>>
            Register([FromBody] UserModel userModel)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Email = userModel.Email,
                UserName = userModel.PhoneNumber,

            }, userModel.Password);

            return new OkObjectResult(result);
        }

        [HttpPost("SendRegisterInformation")]
        public ActionResult EmailRegistration([FromBody] UserModel userModel)
        {
            string base64EncodedPassword = userModel.Password;
            byte[] data = System.Convert.FromBase64String(base64EncodedPassword);
            string base64DecodedPassword = System.Text.ASCIIEncoding.ASCII.GetString(data);

            var customerEmail = userModel.Email;
            var customerName = userModel.FullName;
            var emailSubject = "Hello from Online Request";
            var emailBody = $"Hello {customerName}, your password to login to... is {base64DecodedPassword}, please use this to login...";

            var email = Mailing.CreateEmail(customerEmail, emailSubject, emailBody);
            Mailing.Send(email);

            return Ok();
        }
    }
}
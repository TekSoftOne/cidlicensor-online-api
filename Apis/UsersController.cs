using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OR.Web
{

    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
                UserName = userModel.UserName,
            }, userModel.Password);

            return new OkObjectResult(result);
        }
    }
}
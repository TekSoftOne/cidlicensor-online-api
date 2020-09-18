using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace OR.Web
{
    public class UserCredentialModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtIssuerOptions _jwtOptions;
        public AuthController(
            UserManager<IdentityUser> userManager,
            IOptions<JwtIssuerOptions> jwtOptions
            )
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialModel userCredentials)
        {
            string phoneNumber = userCredentials.PhoneNumber;
            string email = userCredentials.Email;
            string password = userCredentials.Password;

            var identity = await GetClaimsIdentity(phoneNumber, password);
            if (identity == null)
            {
                return BadRequest("Invalid phone number or password.");
            }

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(phoneNumber);

            /// ===== Generate JWT =====
            // Set the timespan the token will be valid for (default is 120 min)
            var validFor = TimeSpan.FromDays(2);

            var userToken = new
            {
                id = identity.Claims.Single(c => c.Type == "Id").Value,
                auth_token = GenerateEncodedToken(phoneNumber, identity.FindFirst("Id"), validFor),
                email = phoneNumber,
                expires_in = validFor.TotalSeconds
            };
            /// ===== END Generate JWT =====

            return new OkObjectResult(userToken);
        }

        private string GenerateEncodedToken(string userName, Claim id, TimeSpan validFor)
        {
            // "jti" (JWT ID) Claim (default ID is a GUID)
            var jti = Guid.NewGuid().ToString();
            //"nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
            var notBefore = DateTime.UtcNow;

            //"iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
            var issuedAt = DateTime.UtcNow;

            var expiration = issuedAt.Add(validFor);
            var signingCredentials = new SigningCredentials(_jwtOptions.SigningKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, jti),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(issuedAt).ToString(), ClaimValueTypes.Integer64),
                 id
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: notBefore,
                expires: expiration,
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
                {
                    new Claim("Id", userToVerify.Id),
                });

            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityByEmail(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty by Phone number
            var userToVerify = _userManager.Users.Where(u => u.Email == email).FirstOrDefault();

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials and add claims
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userToVerify.UserName, "Token"), new[]
                {
                    new Claim("Id", userToVerify.Id),
                });

            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }


    }


}
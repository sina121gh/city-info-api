using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {

        #region DI

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration;

        }

        #endregion

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody
            authenticationRequestBody)
        {
            User? user = ValidateUserCredentials(authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
                return Unauthorized();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"])
                );

            SigningCredentials signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );

            List<Claim> claimsForToken = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            string tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private User? ValidateUserCredentials(string? userName, string? password)
        {
            return _userRepository.GetUserByUserNameAndPassword(userName, password);
        }
    }
}

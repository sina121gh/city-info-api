using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody
            authenticationRequestBody)
        {
            User user = ValidateUserCredentials(authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            return "";
        }

        private User ValidateUserCredentials(string? userName, string? password)
        {
            return null;
        }
    }
}

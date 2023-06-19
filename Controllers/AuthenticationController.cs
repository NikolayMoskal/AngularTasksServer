using MediaItemsServer.Helpers;
using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MediaItemsServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Authenticate(User userData)
        {
            var user = _userService.Get(userData.Name);
            if (user == null)
            {
                return Unauthorized();
            }

            var bearerToken = JwtTokenHelper.GenerateBearerToken(user);
            var refreshToken = JwtTokenHelper.GenerateRefreshToken();

            return new JsonResult(new
            {
                access_token = bearerToken,
                refresh_token = refreshToken,
                expires_in = JwtTokenHelper.LifetimeSeconds
            });
        }
    }
}

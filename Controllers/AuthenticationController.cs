using System.Security.Claims;
using MediaItemsServer.Helpers;
using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaItemsServer.Controllers
{
    [Route(Consts.AuthRoute)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthenticationController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost]
        [Route("login")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Authenticate([FromBody] LoginModel loginModel)
        {
            var user = _userService.Get(loginModel.UserName);
            if (user == null || !user.Password.Equals(loginModel.Password))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };
            claims.AddRange(_roleService.GetRolesForUser(user.Name).Select(x => new Claim(ClaimTypes.Role, x.RoleName)));

            var bearerToken = JwtTokenHelper.GenerateBearerToken(claims);
            var refreshToken = JwtTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            _userService.SaveOrUpdate(user);

            return new JsonResult(new
            {
                access_token = bearerToken,
                refresh_token = refreshToken,
                expires_in = JwtTokenHelper.LifetimeSeconds
            });
        }

        [HttpPost]
        [Route("refresh")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Refresh([FromBody] JwtTokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest("No JWT is present");
            }

            var now = DateTime.UtcNow;
            var principal = JwtTokenHelper.GetPrincipal(tokenModel.BearerToken);

            if (principal?.Identity?.Name == null)
            {
                return BadRequest("Invalid access token");
            }

            var userName = principal.Identity.Name;
            var user = _userService.Get(userName);

            if (user == null || !user.RefreshToken.Equals(tokenModel.RefreshToken) || user.RefreshTokenExpiryTime <= now)
            {
                return BadRequest("Invalid refresh token");
            }

            var bearerToken = JwtTokenHelper.GenerateBearerToken(principal.Claims);
            var refreshToken = JwtTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            _userService.SaveOrUpdate(user);

            return new JsonResult(new
            {
                access_token = bearerToken,
                refresh_token = refreshToken,
                expires_in = JwtTokenHelper.LifetimeSeconds
            });
        }
    }
}

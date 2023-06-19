using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediaItemsServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace MediaItemsServer.Helpers
{
    public static class JwtTokenHelper
    {
        public static readonly int LifetimeSeconds = 10 * 60;
        public static readonly string Issuer = "AngularTasks";
        public static readonly string Audience = "AngularTasks";
        private static readonly string SigningKeyText = "AngularTasksKey";
        private static readonly SecurityKey SigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKeyText));

        public static string GenerateBearerToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(LifetimeSeconds),
                signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public static AuthenticationBuilder AddJwtToken(this AuthenticationBuilder app)
        {
            app.AddJwtBearer(options => options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = SigningKey,
                    ValidateIssuerSigningKey = true
                });

            return app;
        }
    }
}

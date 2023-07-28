using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace MediaItemsServer.Helpers
{
    public static class JwtTokenHelper
    {
        public static readonly int BearerTokenLifetimeSeconds = 10 * 60;
        public static readonly int RefreshTokenLifetimeDays = 1;
        public static readonly string Issuer = "AngularTasks";
        public static readonly string Audience = "AngularTasks";
        private static readonly string SigningKeyText = "AngularTasksTooStrongPasswordForAuthentication";
        private static readonly SecurityKey SigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKeyText));

        public static string GenerateBearerToken(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(BearerTokenLifetimeSeconds),
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

        public static ClaimsPrincipal GetPrincipal(string accessToken)
        {
            return ParseTokenInternal(accessToken, out _);
        }

        public static bool IsTokenExpired(string accessToken)
        {
            ParseTokenInternal(accessToken, out var token);

            return token.ValidTo < DateTime.UtcNow;
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

        private static ClaimsPrincipal ParseTokenInternal(string accessToken, out JwtSecurityToken token)
        {
            token = null;
            var principal = new JwtSecurityTokenHandler().ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = SigningKey
            }, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token provided");

            token = jwtSecurityToken;

            return principal;
        }
    }
}

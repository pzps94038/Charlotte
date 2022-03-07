using Charlotte.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Charlotte.Helper
{
    public static class JwtHelper
    {
        /// <summary>
        /// 產生Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GenerateToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetAppSettingsHelper.GetAppSettingsValue("JWT","Key")));
            string accountTokenExp = GetAppSettingsHelper.GetAppSettingsValue("JWT", "AccountTokenExpirationTime");
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    issuer: GetAppSettingsHelper.GetAppSettingsValue("JWT", "Issuer"),
                    audience: GetAppSettingsHelper.GetAppSettingsValue("JWT", "Audience"),
                    expires: DateTime.Now.AddMinutes(double.Parse(accountTokenExp)),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        /// <summary>
        /// 建立聲明
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Claim> CreateClaims(string email, string userId)
        {
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.NameId, userId)
            };
            return claims;
        }

        /// <summary>
        /// 產生亂數RefreshToken
        /// </summary>
        /// <returns></returns>
        public static string CreateRefreshToken() 
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

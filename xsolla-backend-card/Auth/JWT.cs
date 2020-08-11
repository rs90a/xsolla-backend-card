using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace xsolla_backend_card.Auth
{
    /// <summary>
    /// Класс с параметрами JWT-конфигурации
    /// </summary>
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        
        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="userLogin">Логин пользователя</param>
        /// <returns></returns>
        public string CreateToken(string userLogin)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userLogin", userLogin)
            };
            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
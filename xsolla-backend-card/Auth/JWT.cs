using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        
        //Время жизни токена в минутах
        public int LifeTime { get; set; }
        
        public string UserContext { get; private set; }
        
        /// <summary>
        /// Создание токена
        /// </summary>
        public string CreateToken(string userLogin)
        {
            var signingKey = GetSecurityKey(Key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            UserContext = $"{userLogin}.{(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds}.{Guid.NewGuid()}";
            var claims = new[]
            {
                new Claim("userLogin", userLogin),
                new Claim("userContext", CreateHash(UserContext)), 
            };
            var nowTime = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                nowTime,
                nowTime.Add(TimeSpan.FromMinutes(LifeTime)),
                credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Пользовательский валидатор времени жизни токена.
        /// По умолчанию к expires добавляется константа и время жизни токена получается больше,
        /// т.е. не соответствует значению JWT.LifeTime из appsettings.json
        /// </summary>
        public static bool LifeTimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return expires != null && DateTime.UtcNow < expires;
        }

        /// <summary>
        /// Получение security key
        /// </summary>
        public static SecurityKey GetSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
        
        /// <summary>
        /// Получение хэша SHA1 от строки
        /// </summary>
        public static string CreateHash(string text)
        {
            using var sha1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(
                    sha1.ComputeHash(Encoding.UTF8.GetBytes(text)))
                .Replace("-", "");
        }
    }
}
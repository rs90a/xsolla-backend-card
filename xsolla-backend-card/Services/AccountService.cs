using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using xsolla_backend_card.Auth;
using xsolla_backend_card.Interfaces;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Services
{
    public class AccountService : IAccountService
    {
        private List<User> users = new List<User>
        {
            new User {Login = "admin@gmail.com", Password = "12345"},
            new User {Login = "qwerty@gmail.com", Password = "22222"},
            new User {Login="12345@gmail.com", Password="55555"}
        };

        private readonly JWT jwt;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        public AccountService(IOptions<JWT> jwt, IHttpContextAccessor httpContextAccessor)
        {
            this.jwt = jwt.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetToken(User user)
        {
            RemoveContextCookie();
            UserExists(user);
            var token = jwt.CreateToken(user.Login);
            SetContextCookie();
            return token;
        }

        private void UserExists(User user)
        {
            var foundUser = users.FirstOrDefault(curUser => curUser.Login.Equals(user.Login));
            if (foundUser == null)
                throw new ArgumentException(@$"Пользователя с логином ""{user.Login}"" не существует");
            if (!foundUser.Password.Equals(user.Password))
                throw new ArgumentException("Неверный пароль");
        }

        /// <summary>
        /// Установка пользовательского контекста в cookie
        /// </summary>
        private void SetContextCookie()
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append(
                "userContext",
                jwt.UserContext,
                new CookieOptions()
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(jwt.LifeTime))
                }
            );
        }

        /// <summary>
        /// Удаление пользовательского контекста из cookie (при запрашивании нового токена)
        /// </summary>
        private void RemoveContextCookie()
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append(
                "userContext",
                string.Empty,
                new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddDays(-1)
                }
            );
        }
    }
}
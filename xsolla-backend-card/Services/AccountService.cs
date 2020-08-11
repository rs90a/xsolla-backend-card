using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public AccountService(IOptions<JWT> jwt)
        {
            this.jwt = jwt.Value;
        }

        public string GetToken(User user)
        {
            UserExists(user);
            return jwt.CreateToken(user.Login);
        }

        private void UserExists(User user)
        {
            var foundUser = users.FirstOrDefault(curUser => curUser.Login.Equals(user.Login));
            if (foundUser == null)
                throw new ArgumentException(@$"Пользователя с логином ""{user.Login}"" не существует");
            if (!foundUser.Password.Equals(user.Password))
                throw new ArgumentException("Неверный пароль");
        }
    }
}
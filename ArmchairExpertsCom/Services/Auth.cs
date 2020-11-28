using System.Linq;
using System.Net.Http;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Pages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ArmchairExpertsCom.Services
{
    public static class Auth
    {
        public static bool Registration(
            string login, 
            string name, 
            string surname,
            string birthday, 
            string password,
            string imageName,
            out string authKey)
        {
            var key = Cipher.GetKey(password);
            authKey = Cipher.GetKey(key + login);
            if (Repository.Filter<User>(p => p.Login == login).Any())
                return false;
            
            var user = new User
            {
                Login = login,
                FullName = surname + " " + name,
                BirthDate = birthday,
                PasswordKey = key,
                Role = "user",
            };

            if (!string.IsNullOrEmpty(imageName))
            {
                var image = new Image
                {
                    Path = "\\img" + imageName
                };
                image.Save();
                user.Images.Add(image);
            }
            
            user.Save();
            Repository.SaveChanges();

            return true;
        }

        public static bool TryLogin(string login, string password, out string authKey)
        {
            var key = Cipher.GetKey(password);
            authKey = Cipher.GetKey(key + login);
            return Repository.Get<User>(p => p.Login == login && p.PasswordKey == key) != null;
        }

        private static bool IsCorrectAuthKey(string key, string login, string authKey)
            => authKey == Cipher.GetKey(key + login);

        public static User GetUser(HttpContext context)
        {
            var authKey = context.Session.GetString("authKey");
            return authKey is null
                ? null
                : Repository.Get<User>(u => IsCorrectAuthKey(u.PasswordKey, u.Login, authKey));
        }
    }
}
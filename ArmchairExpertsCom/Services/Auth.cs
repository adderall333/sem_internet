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
                FirstName = name,
                LastName = surname,
                BirthDate = birthday,
                PasswordKey = key,
                Role = "user",
            };

            if (!string.IsNullOrEmpty(imageName))
            {
                var image = new Image
                {
                    Path = "\\img\\" + imageName
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
            var sessionAuthKey = context.Session.GetString("authKey");
            var cookieAuthKey = context.Request.Cookies["authKey"];

            switch (sessionAuthKey)
            {
                case null when cookieAuthKey is null:
                    return null;
                case null:
                    context.Session.SetString("authKey", cookieAuthKey);
                    break;
            }

            return Repository.Get<User>(u => IsCorrectAuthKey(u.PasswordKey, u.Login, sessionAuthKey));
        }

        public static bool IsAuthenticated(HttpContext context)
        {
            return GetUser(context) != null;
        }
        
        public static bool IsOtherUser(HttpContext context) 
            => context.Request.Query["id"].Count > 0 &&
               GetUser(context).Id != int.Parse(context.Request.Query["id"]);
        
        public static bool IsPossibleToSubscribe(HttpContext context) 
            => IsAuthenticated(context) &&
               context.Request.Query["id"].Count > 0 &&
               GetUser(context).Id != int.Parse(context.Request.Query["id"]);

        public static bool IsSubscribed(HttpContext context)
            => IsPossibleToSubscribe(context) && 
               GetUser(context).Subscribes
                .Contains(Repository.Get<User>(u => u.Id == int.Parse(context.Request.Query["id"])));

        public static void EditProfile(User user, string name, string surname, string birthday, string imageName)
        {
            user.FirstName = name ?? user.FirstName;
            user.LastName = surname ?? user.LastName;
            user.BirthDate = birthday ?? user.BirthDate;
            
            if (!string.IsNullOrEmpty(imageName))
            {
                var image = new Image
                {
                    Path = "\\img\\" + imageName
                };
                image.Save();
                user.Images.Clear();
                user.Images.Add(image);
            }
            
            user.Save();
            Repository.SaveChanges();
        }
    }
}
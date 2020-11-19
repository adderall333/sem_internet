using System.Linq;
using System.Net.Http;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
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
            Repository.LoadDataAndRelations();
            var key = Cipher.GetKey(password);
            authKey = key;
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
                user.Images = new DbSet(user, new[] {image});
            }
            
            user.Save();
            Repository.SaveChanges();

            return true;
        }

        public static bool TryLogin(string login, string password, out string authKey)
        {
            Repository.LoadDataAndRelations();
            var key = Cipher.GetKey(password);
            authKey = key;
            return Repository.Get<User>(p => p.Login == login && p.PasswordKey == key) != null;
        }
    }
}
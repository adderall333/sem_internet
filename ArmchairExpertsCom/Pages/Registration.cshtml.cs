using System.IO;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Registration : PageModel
    {
        public string Status { get; private set; }
        
        private IWebHostEnvironment environment;
        
        public Registration(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        
        
        public void OnGet()
        {
            if (HttpContext.Session.GetString("auth_key") != null)
                Status = "Вы уже авторизованы";
        }

        public IActionResult OnPost(
            string login,
            string name, 
            string surname,
            string birthday,
            string password,
            IFormFile image)
        {
            if (HttpContext.Session.GetString("authKey") != null)
            {
                Status = "Вы уже авторизованы";
                return Page();
            }
            
            if (login == null || name == null || surname == null || birthday == null || password == null)
            {
                Status = "Не все поля заполнены";
                return Page();
            }

            if (!Auth.Registration(login, name, surname, birthday, password, image?.FileName, out var authKey))
            {
                Status = "Пользователь с таким Login уже существует";
                return Page();
            }

            if (image != null)
            {
                var path = Path.Combine(environment.ContentRootPath, "wwwroot", "img\\" , image.FileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                image.CopyToAsync(fileStream);
            }
            
            HttpContext.Session.SetString("authKey", authKey);
            return Redirect("/");
        }
    }
}
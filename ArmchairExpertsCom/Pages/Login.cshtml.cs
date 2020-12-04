using System;
using System.Net;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Login : PageModel
    {
        public string Status { get; private set; }
        public string FromUrl { get; private set; }
        
        public void OnGet(string from)
        {
            FromUrl = from;
            if (HttpContext.Session.GetString("authKey") != null)
                Status = "Вы уже авторизованы";
        }

        public IActionResult OnPost(string login, string password, string from, string isPersistent)
        {
            if (HttpContext.Session.GetString("authKey") != null)
            {
                Status = "Вы уже авторизованы";
                return Page();
            }
            
            if (login == null || password == null)
            {
                Status = "Не все поля заполнены";
                return Page();
            }

            if (!Auth.TryLogin(login, password, out var authKey))
            {
                Status = "Неверный логин или пароль";
                return Page();
            }

            //todo
            if (isPersistent == "on")
            {
                HttpContext.Response.Cookies.Delete("authKey");
                HttpContext.Response.Cookies.Append("authKey", authKey, 
                    new CookieOptions {Expires = DateTimeOffset.MaxValue});
            }
            else
            {
                HttpContext.Session.SetString("authKey", authKey);
            }
            
            return Redirect($"/{from}");
        }
    }
}
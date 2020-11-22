using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Profile
{
    public class Friends : PageModel
    {
        public User CurrentUser { get; private set; }
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect("/login?from=profile");
            
            var authKey = HttpContext.Session.GetString("authKey");
                
            Repository.LoadDataAndRelations();
            CurrentUser = Repository.Get<User>(user => user.PasswordKey == authKey);
            return Page();
        }
    }
}
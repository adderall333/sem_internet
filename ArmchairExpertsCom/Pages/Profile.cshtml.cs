using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Profile : PageModel
    {
        public User User { get; private set; }
        
        public IActionResult OnGet()
        {
            var authKey = HttpContext.Session.GetString("authKey");
            if (authKey == null)
                return Redirect("/login?from=profile");
                
            Repository.LoadDataAndRelations();
            User = Repository.Get<User>(user => user.PasswordKey == HttpContext.Session.GetString("authKey"));
            return Page();
        }
    }
}
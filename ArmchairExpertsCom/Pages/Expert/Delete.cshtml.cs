using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Delete : PageModel
    {
        public IActionResult OnGet()
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect("/login?from=expert");
            
            UserActions.DeleteProfile(user);
            
            return Redirect("/Index");
        }
    }
}
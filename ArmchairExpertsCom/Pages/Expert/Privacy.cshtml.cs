using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Privacy : PageModel
    {
        public User Expert { get; private set; }
        
        public IActionResult OnGet()
        {
            Expert = Auth.GetUser(HttpContext);
            
            if (Expert is null)
                return Redirect("/login?from=expert");

            return Page();
        }

        public IActionResult OnPost(
            string watched,
            string subscribes,
            string reviews,
            string selections,
            string pending)
        {
            Expert = Auth.GetUser(HttpContext);
            
            if (Expert is null)
                return Redirect("/login?from=expert");
            
            UserActions.ChangePrivacySettings(
                Expert,
                watched == "on",
                subscribes == "on",
                reviews == "on",
                selections == "on",
                pending == "on");

            return Redirect("/Expert/Index");
        }
    }
}
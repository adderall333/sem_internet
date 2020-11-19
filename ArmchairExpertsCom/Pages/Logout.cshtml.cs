using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
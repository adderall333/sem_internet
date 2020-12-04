using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Selections
{
    public class Details : PageModel
    {
        public Selection Selection { get; private set; }
        
        public void OnGet(int id)
        {
            Selection = Repository.Get<Selection>(s => s.Id == id);
        }

        public IActionResult OnPost(string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=selections/details?id={Request.Query["id"]}");
            
            Selection = Repository.Get<Selection>(book => book.Id == int.Parse(Request.Query["id"]));
            
            UserActions.WriteComment(user, Selection, text);
            
            return Redirect(Request.QueryString.Value);
        }
    }
}
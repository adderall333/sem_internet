using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Profile
{
    public class Serials : PageModel
    {
        public User CurrentUser { get; private set; }
        public IEnumerable<SerialEvaluation> Evaluations { get; private set; }
        
        public IActionResult OnGet()
        {
            CurrentUser = Auth.GetUser(HttpContext);
            
            if (CurrentUser is null)
                return Redirect("/login?from=profile/serials");
            
            Evaluations = Repository.Filter<SerialEvaluation>(se => se.User.First() == CurrentUser);
            return Page();
        }
    }
}
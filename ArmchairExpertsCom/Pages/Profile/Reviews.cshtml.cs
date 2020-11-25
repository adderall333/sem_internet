using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Profile
{
    public class Reviews : PageModel
    {
        public User CurrentUser { get; private set; }
        public IEnumerable<IReview> AllReviews { get; private set; }
        
        public IActionResult OnGet()
        {
            CurrentUser = Auth.GetUser(HttpContext);
            
            if (CurrentUser is null)
                return Redirect("/login?from=profile/reviews");
            
            AllReviews = Repository
                .GetModelsByType(typeof(BookReview), false)
                .Select(r => (IReview) r)
                .Concat(Repository
                    .GetModelsByType(typeof(FilmReview), false)
                    .Select(r => (IReview) r))
                .Concat(Repository
                    .GetModelsByType(typeof(SerialReview), false)
                    .Select(r => (IReview) r));
            return Page();
        }
    }
}
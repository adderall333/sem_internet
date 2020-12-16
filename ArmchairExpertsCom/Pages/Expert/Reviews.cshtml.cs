using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Reviews : PageModel
    {
        public User Expert { get; private set; }
        public List<IReview> AllReviews { get; private set; }
        
        public IActionResult OnGet(int? id)
        {
            if (!Auth.IsOtherUser(HttpContext))
            {
                Expert = Auth.GetUser(HttpContext);
            
                if (Expert is null)
                    return Redirect("/login?from=expert/reviews");
            }
            else
            {
                Expert = Repository.Get<User>(u => u.Id == id);
                if (!((PrivacySettings) Expert.Privacy.First()).AreReviewsOpen)
                    return Redirect($"/Expert/PrivacyLimit?id={id}");
            }
            
            AllReviews = Repository
                .All<BookReview>()
                .Select(r => (IReview) r)
                .Concat(Repository
                    .All<FilmReview>()
                    .Select(r => (IReview) r))
                .Concat(Repository
                    .All<SerialReview>()
                    .Select(r => (IReview) r))
                .Where(r => r.User.First() == Expert)
                .ToList();
            return Page();
        }
    }
}
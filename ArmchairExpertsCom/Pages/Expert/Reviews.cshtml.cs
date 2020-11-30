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
        public IEnumerable<IReview> AllReviews { get; private set; }
        
        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                Expert = Auth.GetUser(HttpContext);
            
                if (Expert is null)
                    return Redirect("/login?from=expert/reviews");
            }
            else
            {
                Expert = Repository.Get<User>(u => u.Id == id);
            }
            
            AllReviews = Repository
                .GetModelsByType(typeof(BookReview), false)
                .Select(r => (IReview) r)
                .Concat(Repository
                    .GetModelsByType(typeof(FilmReview), false)
                    .Select(r => (IReview) r))
                .Concat(Repository
                    .GetModelsByType(typeof(SerialReview), false)
                    .Select(r => (IReview) r))
                .Where(r => r.User.First() == Expert);
            return Page();
        }
        
        public IActionResult OnPost(int id)
        {
            var subscriber = Auth.GetUser(HttpContext);
            
            if (subscriber is null)
                return Redirect("/login?from=expert");

            var subscribe = Repository.Get<User>(u => u.Id == id);

            if (Auth.IsSubscribed(HttpContext))
            {
                UserActions.UnSubscribe(subscriber, subscribe);
            }
            else
            {
                UserActions.Subscribe(subscriber, subscribe);
            }

            return Redirect(Request.QueryString.Value);
        }
    }
}
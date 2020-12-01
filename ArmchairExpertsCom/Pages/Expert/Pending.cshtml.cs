using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Pending : PageModel
    {
        public User Expert { get; private set; }
        public IEnumerable<IArtwork> Artworks { get; private set; } 
        
        public IActionResult OnGet(int? id)
        {
            if (!Auth.IsOtherUser(HttpContext))
            {
                Expert = Auth.GetUser(HttpContext);
            
                if (Expert is null)
                    return Redirect("/login?from=expert/pending");
            }
            else
            {
                Expert = Repository.Get<User>(u => u.Id == id);
                if (!((PrivacySettings) Expert.Privacy.First()).ArePendingOpen)
                    return Redirect($"/Expert/PrivacyLimit?id={id}");
            }

            Artworks = Expert
                .PendingBooks
                .Concat(Expert.PendingFilms)
                .Concat(Expert.PendingSerials)
                .Select(a => (IArtwork) a);
            
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
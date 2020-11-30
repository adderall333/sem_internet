using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Selections : PageModel
    {
        public User Expert { get; private set; }
        public List<Selection> AllSelections { get; private set; }
        
        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                Expert = Auth.GetUser(HttpContext);
            
                if (Expert is null)
                    return Redirect("/login?from=expert/selections");
            }
            else
            {
                Expert = Repository.Get<User>(u => u.Id == id);
            }

            AllSelections = Expert
                .Selections
                .Select(s => (Selection) s)
                .ToList();

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
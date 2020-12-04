using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Unsubscribe : PageModel
    {
        public IActionResult OnPost(int id)
        {
            var subscriber = Auth.GetUser(HttpContext);
            
            if (subscriber is null)
                return Redirect("/login?from=expert");

            var subscribe = Repository.Get<User>(u => u.Id == id);

            if (subscriber.Subscribes.Contains(subscribe))
            {
                UserActions.UnSubscribe(subscriber, subscribe);
            }
            else
            {
                UserActions.Subscribe(subscriber, subscribe);
            }

            return Redirect($"/expert?id={id}");
        }
    }
}
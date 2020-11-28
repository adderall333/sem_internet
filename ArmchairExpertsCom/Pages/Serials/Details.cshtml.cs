using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Details : PageModel
    {
        public Serial Serial { get; private set; }
        public List<Serial> SimilarSerials { get; private set; }
        
        public void OnGet(int id)
        {
            Serial = Repository.Get<Serial>(serial => serial.Id == id);
            SimilarSerials = ContentMaker.GetSimilarSerials(Serial).ToList();
        }

        public IActionResult OnPost(int value, string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=serials/details?id={Request.Query["id"]}");

            Serial = Repository.Get<Serial>(serial => serial.Id == int.Parse(Request.Query["id"]));

            UserActions.WriteReview(user, Serial, text, value);

            return Redirect(Request.QueryString.Value);
        }
    }
}
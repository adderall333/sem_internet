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
        public int? Evaluation { get; private set; }
        public bool isPending { get; private set; }
        
        public void OnGet(int id)
        {
            Serial = Repository.Get<Serial>(serial => serial.Id == id);
            SimilarSerials = ContentMaker.GetSimilarSerials(Serial).ToList();
            
            var user = Auth.GetUser(HttpContext);
            
            if (user == null) return;
            Evaluation = Repository
                .Get<SerialEvaluation>(e => e.User.First() == user && e.Serial.First() == Serial)?.Value;
            isPending = user.PendingSerials.Contains(Serial);
        }
        
        public IActionResult OnPostReview(int value, string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=serials/details?id={Request.Query["id"]}");

            Serial = Repository.Get<Serial>(serial => serial.Id == int.Parse(Request.Query["id"]));

            UserActions.WriteReview(user, Serial, text, value);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostDelay()
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=serials/details?id={Request.Query["id"]}");

            Serial = Repository.Get<Serial>(serial => serial.Id == int.Parse(Request.Query["id"]));

            if (user.PendingSerials.Contains(Serial))
                UserActions.UnDelay(user, Serial);
            else
                UserActions.Delay(user, Serial);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostEvaluate(int value)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=serials/details?id={Request.Query["id"]}");

            Serial = Repository.Get<Serial>(serial => serial.Id == int.Parse(Request.Query["id"]));

            UserActions.Evaluate(user, Serial, value);

            return Redirect(Request.QueryString.Value);
        }
    }
}
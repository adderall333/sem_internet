using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Admin
{
    public class Edit : PageModel
    {
        public IModel Model { get; private set; }
        
        public IActionResult OnGet(string type, int id)
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect($"/login?from=admin/models?type={type}");
            
            if (!Administration.IsAdmin(HttpContext))
                return Forbid();
            
            Model = Repository.GetModelsByTypeName(type, true).First(m => m.Id == id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect($"/login?from=admin/models?type={Request.Query["type"].First()}");
            
            if (!Administration.IsAdmin(HttpContext))
                return Forbid();
            
            Administration.EditModel(
                Request.Query["type"].First(),
                int.Parse(Request.Query["id"].First()),
                Request.Form);
            
            return Redirect(Request.QueryString.ToUriComponent());
        }
    }
}
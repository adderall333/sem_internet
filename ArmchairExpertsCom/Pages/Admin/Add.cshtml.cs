using System;
using System.Linq;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Admin
{
    public class Add : PageModel
    {
        public IModel Model { get; private set; }
        
        public IActionResult OnGet(string type)
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect($"/login?from=admin/models?type={type}");
            
            if (!Administration.IsAdmin(HttpContext))
                return Forbid();
            
            Model = (IModel) Activator.CreateInstance(Repository.GetType(type));

            return Page();
        }

        public IActionResult OnPost(string type, int id)
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect($"/login?from=admin/models?type={Request.Query["type"].First()}");
            
            if (!Administration.IsAdmin(HttpContext))
                return Forbid();
            
            var model = Administration.CreateModel(
                Request.Query["type"].First(),
                Request.Form);
            
            return Redirect($"/Admin/Models?type={type}");
        }
    }
}
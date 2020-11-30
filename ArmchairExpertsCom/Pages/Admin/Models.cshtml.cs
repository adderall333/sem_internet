using System;
using System.Collections.Generic;
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
    public class Models : PageModel
    {
        public List<IModel> ModelsList { get; private set; }
        public Type Type { get; private set; }
        
        public IActionResult OnGet(string type)
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect($"/login?from=admin/models?type={type}");
            
            if (!Administration.IsAdmin(HttpContext))
                return Forbid();

            Type = Repository.GetType(type);
            ModelsList = Repository.GetModelsByTypeName(type, true);
            return Page();
        }
    }
}
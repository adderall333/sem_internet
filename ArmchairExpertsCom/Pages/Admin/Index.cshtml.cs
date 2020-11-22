using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Admin
{
    public class Index : PageModel
    {
        public Dictionary<Type, List<IModel>> AllModels { get; private set; }
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("authKey") is null)
                return Redirect("/login?from=admin");

            if (!Administration.IsAdmin(HttpContext.Session.GetString("authKey")))
                return Forbid();

            AllModels = Repository.GetAdminCreatedData();
            return Page();
        }
    }
}
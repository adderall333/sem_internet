﻿using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Expert
{
    public class Books : PageModel
    {
        public User Expert { get; private set; }
        public IEnumerable<BookEvaluation> Evaluations { get; private set; }
        
        public IActionResult OnGet(int? id)
        {
            if (!Auth.IsOtherUser(HttpContext))
            {
                Expert = Auth.GetUser(HttpContext);
            
                if (Expert is null)
                    return Redirect("/login?from=expert/books");
            }
            else
            {
                Expert = Repository.Get<User>(u => u.Id == id);
                if (!((PrivacySettings) Expert.Privacy.First()).AreWatchedOpen)
                    return Redirect($"/Expert/PrivacyLimit?id={id}");
            }
            
            Evaluations = Repository.Filter<BookEvaluation>(be => be.User.First() == Expert);
            return Page();
        }
    }
}
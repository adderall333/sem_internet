using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Selections
{
    public class Index : PageModel
    { 
        public List<Selection> AllSelections { get; private set; }
        
        public void OnGet()
        {
            AllSelections = Repository.All<Selection>().ToList();
        }
    }
}
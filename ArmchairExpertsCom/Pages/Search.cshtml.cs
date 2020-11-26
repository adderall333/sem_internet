using System.Collections.Generic;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Search : PageModel
    {
        public IEnumerable<IArtwork> FoundModels { get; private set; }
        
        public void OnGet(string searchString)
        {
        }
        
        public void OnPost(string searchString)
        {
            FoundModels = ContentMaker.SearchAll(searchString);
        }
    }
}
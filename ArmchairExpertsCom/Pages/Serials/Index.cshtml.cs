using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Index : PageModel
    {
        public IEnumerable<Serial> AllSerials { get; private set; }
        
        public void OnGet()
        {
            AllSerials = Repository.All<Serial>();
        }

        public void OnPost(string searchString)
        {
            AllSerials = ContentMaker.SearchSerials(searchString);
        }
    }
}
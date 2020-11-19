using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Index : PageModel
    {
        public IEnumerable<Serial> AllSerials { get; private set; }
        
        public void OnGet()
        {
            Repository.LoadDataAndRelations();
            AllSerials = Repository.All<Serial>();
        }
    }
}
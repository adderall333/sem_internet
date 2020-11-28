using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Selections
{
    public class Details : PageModel
    {
        public Selection Selection { get; private set; }
        
        public void OnGet(int id)
        {
            Selection = Repository.Get<Selection>(s => s.Id == id);
        }
    }
}
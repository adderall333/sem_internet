using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Details : PageModel
    {
        public Serial Serial { get; private set; }
        
        public void OnGet(int id)
        {
            Repository.LoadDataAndRelations();
            Serial = Repository.Get<Serial>(serial => serial.Id == id);
        }
    }
}
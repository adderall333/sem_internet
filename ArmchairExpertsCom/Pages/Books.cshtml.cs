using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Pages.Models;
using ArmchairExpertsCom.Pages.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Books : PageModel
    {
        public IEnumerable<Book> AllBooks { get; private set; }
        
        public void OnGet()
        {
            Repository.LoadData();
            Repository.LoadRelations();
            AllBooks = Repository.All<Book>();
        }
    }
}
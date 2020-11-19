using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Index : PageModel
    {
        public IEnumerable<Book> AllBooks { get; private set; }
        
        public void OnGet()
        {
            Repository.LoadDataAndRelations();
            AllBooks = Repository.All<Book>();
        }
    }
}
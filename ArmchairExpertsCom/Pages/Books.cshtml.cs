using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Books : PageModel
    {
        public IEnumerable<Book> AllBooks { get; private set; }
        
        public void OnGet()
        {
            AllBooks = Mocks.All<Book>();
        }
    }
}
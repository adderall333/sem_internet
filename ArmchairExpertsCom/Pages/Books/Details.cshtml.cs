using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Details : PageModel
    {
        public Book Book { get; private set; }
        
        public void OnGet(int id)
        {
            Repository.LoadDataAndRelations();
            Book = Repository.Get<Book>(book => book.Id == id);
        }
    }
}
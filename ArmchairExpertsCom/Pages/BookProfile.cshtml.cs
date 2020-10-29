using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class BookProfile : PageModel
    {
        public Book Book { get; private set; }
        
        public void OnGet(int id)
        {
            Book = Mocks.Get<Book>(id);
        }
    }
}
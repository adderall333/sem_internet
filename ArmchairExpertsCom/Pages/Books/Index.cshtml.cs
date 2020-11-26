using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Index : PageModel
    {
        public IEnumerable<Book> AllBooks { get; private set; }
        
        public void OnGet()
        {
            AllBooks = Repository.All<Book>();
        }

        public void OnPost(string searchString)
        {
            AllBooks = ContentMaker.SearchBooks(searchString);
        }
    }
}
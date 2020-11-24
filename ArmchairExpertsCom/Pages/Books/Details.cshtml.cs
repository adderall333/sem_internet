using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Details : PageModel
    {
        public Book Book { get; private set; }
        public List<Book> SimilarBooks { get; private set; }
        
        public void OnGet(int id)
        {
            Repository.LoadDataAndRelations();
            Book = Repository.Get<Book>(book => book.Id == id);
            SimilarBooks = ContentMaker.GetSimilarBooks(Book).ToList();
        }
    }
}
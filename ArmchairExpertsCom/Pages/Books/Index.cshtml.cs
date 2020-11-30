using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Index : PageModel
    {
        public IEnumerable<Book> AllBooks { get; private set; }
        
        public void OnGet(int? genreId, string sort)
        {
            AllBooks = genreId is null ? Repository.All<Book>() : Repository
                .Get<BookGenre>(g => g.Id == genreId)
                .Books
                .Select(e => (Book) e);;
            
            AllBooks = sort switch
            {
                "alphabet" => AllBooks.OrderBy(e => e.Title),
                "rating" => AllBooks.OrderBy(ContentMaker.GetRating),
                "new" => AllBooks.OrderByDescending(e => e.Year),
                "old" => AllBooks.OrderBy(e => e.Year),
                _ => AllBooks
            };
        }

        public void OnPost(string searchString)
        {
            AllBooks = ContentMaker.SearchBooks(searchString);
        }
    }
}
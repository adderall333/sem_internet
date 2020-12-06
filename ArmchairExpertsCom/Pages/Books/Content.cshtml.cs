using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Content : PageModel
    {
        public IEnumerable<Book> Books { get; private set; }
        
        public void OnGet(int? genreId, string sort, string searchString, int count)
        {
            Books = genreId is null
                ? ContentMaker.SearchBooks(searchString)
                : Repository
                    .Get<BookGenre>(g => g.Id == genreId)
                    .Books
                    .Select(e => (Book) e)
                    .Intersect(ContentMaker.SearchBooks(searchString));
            
            Books = sort switch
            {
                "alphabet" => Books.OrderBy(e => e.Title),
                "rating" => Books.OrderBy(ContentMaker.GetRating),
                "new" => Books.OrderByDescending(e => e.Year),
                "old" => Books.OrderBy(e => e.Year),
                _ => Books
            };

            Books = Books.Skip(count).Take(5);
        }
    }
}
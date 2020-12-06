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
        public int BooksCount { get; private set; }
        
        public void OnGet(int? genreId, string searchString)
        {
            BooksCount = genreId is null
                ? ContentMaker.SearchBooks(searchString).Count()
                : Repository
                    .Get<BookGenre>(g => g.Id == genreId)
                    .Books
                    .Select(e => (Book) e)
                    .Intersect(ContentMaker.SearchBooks(searchString))
                    .Count();
        }
    }
}
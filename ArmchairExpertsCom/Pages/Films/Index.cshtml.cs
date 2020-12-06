using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Index : PageModel
    {
        public int FilmsCount { get; private set; }
        
        public void OnGet(int? genreId, string searchString)
        {
            FilmsCount = genreId is null
                ? ContentMaker.SearchBooks(searchString).Count()
                : Repository
                    .Get<FilmGenre>(g => g.Id == genreId)
                    .Films
                    .Select(e => (Film) e)
                    .Intersect(ContentMaker.SearchFilms(searchString))
                    .Count();
        }
    }
}
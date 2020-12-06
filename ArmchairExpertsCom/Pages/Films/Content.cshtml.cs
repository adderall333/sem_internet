using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Content : PageModel
    {
        public IEnumerable<Film> Films { get; private set; }
        
        public void OnGet(int? genreId, string sort, string searchString, int count)
        {
            Films = genreId is null
                ? ContentMaker.SearchFilms(searchString)
                : Repository
                    .Get<FilmGenre>(g => g.Id == genreId)
                    .Films
                    .Select(e => (Film) e)
                    .Intersect(ContentMaker.SearchFilms(searchString));
            
            Films = sort switch
            {
                "alphabet" => Films.OrderBy(e => e.Title),
                "rating" => Films.OrderBy(ContentMaker.GetRating),
                "new" => Films.OrderByDescending(e => e.Year),
                "old" => Films.OrderBy(e => e.Year),
                _ => Films
            };

            Films = Films.Skip(count).Take(5);
        }
    }
}
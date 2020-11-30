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
        public IEnumerable<Film> AllFilms { get; private set; }
        
        public void OnGet(int? genreId, string sort)
        {
            AllFilms = genreId is null ? Repository.All<Film>() : Repository
                .Get<FilmGenre>(g => g.Id == genreId)
                .Films
                .Select(e => (Film) e);
            
            AllFilms = sort switch
            {
                "alphabet" => AllFilms.OrderBy(e => e.Title),
                "rating" => AllFilms.OrderBy(ContentMaker.GetRating),
                "new" => AllFilms.OrderByDescending(e => e.Year),
                "old" => AllFilms.OrderBy(e => e.Year),
                _ => AllFilms
            };
        }

        public void OnPost(string searchString)
        {
            AllFilms = ContentMaker.SearchFilms(searchString);
        }
    }
}
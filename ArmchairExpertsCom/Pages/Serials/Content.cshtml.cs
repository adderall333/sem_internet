using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Content : PageModel
    {
        public IEnumerable<Serial> Serials { get; private set; }
        
        public void OnGet(int? genreId, string sort, string searchString, int count)
        {
            Serials = genreId is null
                ? ContentMaker.SearchSerials(searchString)
                : Repository
                    .Get<SerialGenre>(g => g.Id == genreId)
                    .Serials
                    .Select(e => (Serial) e)
                    .Intersect(ContentMaker.SearchSerials(searchString));
            
            Serials = sort switch
            {
                "alphabet" => Serials.OrderBy(e => e.Title),
                "rating" => Serials.OrderBy(ContentMaker.GetRating),
                "new" => Serials.OrderByDescending(e => e.StartYear),
                "old" => Serials.OrderBy(e => e.StartYear),
                _ => Serials
            };

            Serials = Serials.Skip(count).Take(5);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Serials
{
    public class Index : PageModel
    {
        public IEnumerable<Serial> AllSerials { get; private set; }
        
        public void OnGet(int? genreId, string sort, string searchString)
        {
            AllSerials = genreId is null ? ContentMaker.SearchSerials(searchString) : Repository
                .Get<SerialGenre>(g => g.Id == genreId)
                .Serials
                .Select(e => (Serial) e);
            
            AllSerials = sort switch
            {
                "alphabet" => AllSerials.OrderBy(e => e.Title),
                "rating" => AllSerials.OrderBy(ContentMaker.GetRating),
                "new" => AllSerials.OrderByDescending(e => e.StartYear),
                "old" => AllSerials.OrderBy(e => e.StartYear),
                _ => AllSerials
            };
        }
    }
}
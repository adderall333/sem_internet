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
        public int SerialsCount { get; private set; }
        
        public void OnGet(int? genreId, string searchString)
        {
            SerialsCount = genreId is null
                ? ContentMaker.SearchBooks(searchString).Count()
                : Repository
                    .Get<SerialGenre>(g => g.Id == genreId)
                    .Serials
                    .Select(e => (Serial) e)
                    .Intersect(ContentMaker.SearchSerials(searchString))
                    .Count();
        }
    }
}
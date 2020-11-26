using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Index : PageModel
    {
        public IEnumerable<Film> AllFilms { get; private set; }
        
        public void OnGet()
        {
            AllFilms = Repository.All<Film>();
        }

        public void OnPost(string searchString)
        {
            AllFilms = ContentMaker.SearchFilms(searchString);
        }
    }
}
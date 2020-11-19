using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Index : PageModel
    {
        public IEnumerable<Film> AllFilms { get; private set; }
        
        public void OnGet()
        {
            Repository.LoadDataAndRelations();
            AllFilms = Repository.All<Film>();
        }
    }
}
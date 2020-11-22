using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Details : PageModel
    {
        public Film Film { get; private set; }
        public List<Film> SimilarFilms { get; private set; }
        
        public void OnGet(int id)
        {
            Repository.LoadDataAndRelations();
            Film = Repository.Get<Film>(film => film.Id == id);
            SimilarFilms = null; //todo
        }
    }
}
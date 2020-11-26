using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Films
{
    public class Details : PageModel
    {
        public Film Film { get; private set; }
        public List<Film> SimilarFilms { get; private set; }
        
        public void OnGet(int id)
        {
            Film = Repository.Get<Film>(film => film.Id == id);
            SimilarFilms = ContentMaker.GetSimilarFilms(Film).ToList();
        }
    }
}
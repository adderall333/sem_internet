using System;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
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
            var film = Repository.All<FilmEvaluation>().First().Film.First();
            var details = Repository
                .Filter<FilmEvaluation>(fe => fe.Film.First() == Film).ToList();
        }
        
        public IActionResult OnPost(int value, string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=films/details?id={Request.Query["id"]}");

            Film = Repository.Get<Film>(film => film.Id == int.Parse(Request.Query["id"]));

            UserActions.WriteReview(user, Film, text, value);

            return Redirect(Request.QueryString.Value);
        }
    }
}
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
        public int? Evaluation { get; private set; }
        public bool IsPending { get; private set; }
        
        public void OnGet(int id)
        {
            Film = Repository.Get<Film>(film => film.Id == id);
            SimilarFilms = ContentMaker.GetSimilarFilms(Film).ToList();
            
            var user = Auth.GetUser(HttpContext);
            
            if (user == null) return;
            Evaluation = Repository
                .Get<FilmEvaluation>(e => e.User.First() == user && e.Film.First() == Film)?.Value;
            IsPending = user.PendingFilms.Contains(Film);
        }

        public IActionResult OnPostReview(int value, string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=films/details?id={Request.Query["id"]}");

            Film = Repository.Get<Film>(film => film.Id == int.Parse(Request.Query["id"]));

            UserActions.WriteReview(user, Film, text, value);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostDelay()
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=films/details?id={Request.Query["id"]}");

            Film = Repository.Get<Film>(film => film.Id == int.Parse(Request.Query["id"]));

            if (user.PendingFilms.Contains(Film))
                UserActions.UnDelay(user, Film);
            else
                UserActions.Delay(user, Film);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostEvaluate(int value)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=films/details?id={Request.Query["id"]}");

            Film = Repository.Get<Film>(film => film.Id == int.Parse(Request.Query["id"]));

            UserActions.Evaluate(user, Film, value);

            return Redirect(Request.QueryString.Value);
        }
    }
}
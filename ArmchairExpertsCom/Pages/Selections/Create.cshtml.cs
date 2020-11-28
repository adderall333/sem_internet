using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Selections
{
    public class Create : PageModel
    {
        public User User { get; private set; }
        public IEnumerable<Book> ReadBooks { get; private set; } 
        public IEnumerable<Film> WatchedFilms { get; private set; } 
        public IEnumerable<Serial> WatchedSerials { get; private set; } 
        
        public IActionResult OnGet()
        {
            User = Auth.GetUser(HttpContext);
        
            if (User is null)
                return Redirect("/login?from=selections/create");

            ReadBooks = Repository
                .Filter<BookEvaluation>(be => be.User.First() == User)
                .Select(be => (Book) be.Book.First());
            
            WatchedFilms = Repository
                .Filter<FilmEvaluation>(fe => fe.User.First() == User)
                .Select(fe => (Film) fe.Film.First());
            
            WatchedSerials = Repository
                .Filter<SerialEvaluation>(se => se.User.First() == User)
                .Select(se => (Serial) se.Serial.First());

            return Page();
        }
    }
}
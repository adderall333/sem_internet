using System;
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
        public List<Book> ReadBooks { get; private set; } 
        public List<Film> WatchedFilms { get; private set; } 
        public List<Serial> WatchedSerials { get; private set; } 
        
        [BindProperty]
        public List<int> Books { get; set; }
        
        [BindProperty]
        public List<int> Films { get; set; }
        
        [BindProperty]
        public List<int> Serials { get; set; }
        
        public IActionResult OnGet()
        {
            User = Auth.GetUser(HttpContext);
        
            if (User is null)
                return Redirect("/login?from=selections/create");

            ReadBooks = Repository
                .Filter<BookEvaluation>(be => be.User.First() == User)
                .Select(be => (Book) be.Book.First())
                .ToList();
            
            WatchedFilms = Repository
                .Filter<FilmEvaluation>(fe => fe.User.First() == User)
                .Select(fe => (Film) fe.Film.First())
                .ToList();
            
            WatchedSerials = Repository
                .Filter<SerialEvaluation>(se => se.User.First() == User)
                .Select(se => (Serial) se.Serial.First())
                .ToList();

            return Page();
        }

        public IActionResult OnPost(string title, string text)
        {
            User = Auth.GetUser(HttpContext);
            
            if (User is null)
                return Redirect("/login?from=selections/create");
            
            ReadBooks = Repository
                .Filter<BookEvaluation>(be => be.User.First() == User)
                .Select(be => (Book) be.Book.First())
                .ToList();
            
            WatchedFilms = Repository
                .Filter<FilmEvaluation>(fe => fe.User.First() == User)
                .Select(fe => (Film) fe.Film.First())
                .ToList();
            
            WatchedSerials = Repository
                .Filter<SerialEvaluation>(se => se.User.First() == User)
                .Select(se => (Serial) se.Serial.First())
                .ToList();

            var selection = UserActions.CreateSelection(User, title, text);
            foreach (var book in Books.Select(b => ReadBooks[b]))
            {
                UserActions.AddToSelection(User, selection, book);
            }
            foreach (var film in Films.Select(f => WatchedFilms[f]))
            {
                UserActions.AddToSelection(User, selection, film);
            }
            foreach (var serial in Serials.Select(s => WatchedSerials[s]))
            {
                UserActions.AddToSelection(User, selection, serial);
            }
            
            return Page();
        }
    }
}
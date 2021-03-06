﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Selections
{
    public class Create : PageModel
    {
        public User CurrentUser { get; private set; }
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
            CurrentUser = Auth.GetUser(HttpContext);
        
            if (CurrentUser is null)
                return Redirect("/login?from=selections/create");

            ReadBooks = Repository
                .Filter<BookEvaluation>(be => be.User.First() == CurrentUser)
                .Select(be => (Book) be.Book.First())
                .ToList();
            
            WatchedFilms = Repository
                .Filter<FilmEvaluation>(fe => fe.User.First() == CurrentUser)
                .Select(fe => (Film) fe.Film.First())
                .ToList();
            
            WatchedSerials = Repository
                .Filter<SerialEvaluation>(se => se.User.First() == CurrentUser)
                .Select(se => (Serial) se.Serial.First())
                .ToList();

            return Page();
        }

        public IActionResult OnPost(string title, string text)
        {
            CurrentUser = Auth.GetUser(HttpContext);
            
            if (CurrentUser is null)
                return Redirect("/login?from=selections/create");
            
            ReadBooks = Repository
                .Filter<BookEvaluation>(be => be.User.First() == CurrentUser)
                .Select(be => (Book) be.Book.First())
                .ToList();
            
            WatchedFilms = Repository
                .Filter<FilmEvaluation>(fe => fe.User.First() == CurrentUser)
                .Select(fe => (Film) fe.Film.First())
                .ToList();
            
            WatchedSerials = Repository
                .Filter<SerialEvaluation>(se => se.User.First() == CurrentUser)
                .Select(se => (Serial) se.Serial.First())
                .ToList();

            var selection = UserActions.CreateSelection(CurrentUser, title, text);
            
            var artworks = Books.Select(b => ReadBooks[b]).Cast<IArtwork>().ToList();
            artworks.AddRange(Films.Select(f => WatchedFilms[f]));
            artworks.AddRange(Serials.Select(s => WatchedSerials[s]));

            if (artworks.Count < 3) return Page();
            foreach (var artwork in artworks)
            {
                UserActions.AddToSelection(CurrentUser, selection, artwork);
            }

            return Redirect($"/selections/details?id={selection.Id}");
        }
    }
}
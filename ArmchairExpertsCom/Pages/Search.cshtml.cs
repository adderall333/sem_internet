using System.Collections.Generic;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages
{
    public class Search : PageModel
    {
        public IEnumerable<User> FoundUsers { get; private set; }
        public IEnumerable<Book> FoundBooks { get; private set; }
        public IEnumerable<Film> FoundFilms { get; private set; }
        public IEnumerable<Serial> FoundSerials { get; private set; }
        
        public void OnGet(string searchString)
        {
            FoundUsers = ContentMaker.SearchUsers(searchString);
            FoundBooks = ContentMaker.SearchBooks(searchString);
            FoundFilms = ContentMaker.SearchFilms(searchString);
            FoundSerials = ContentMaker.SearchSerials(searchString);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Utilities;
using ArmchairExpertsCom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArmchairExpertsCom.Pages.Books
{
    public class Details : PageModel
    {
        public Book Book { get; private set; }
        public List<Book> SimilarBooks { get; private set; }
        public int? Evaluation { get; private set; }
        public bool IsPending { get; private set; }
        
        public void OnGet(int id)
        {
            Book = Repository.Get<Book>(book => book.Id == id);
            SimilarBooks = ContentMaker.GetSimilarBooks(Book).ToList();
            
            var user = Auth.GetUser(HttpContext);
            
            if (user == null) return;
            Evaluation = Repository
                .Get<BookEvaluation>(e => e.User.First() == user && e.Book.First() == Book)?.Value;
            IsPending = user.PendingBooks.Contains(Book);
        }
        
        public IActionResult OnPostReview(int value, string text)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=books/details?id={Request.Query["id"]}");

            Book = Repository.Get<Book>(book => book.Id == int.Parse(Request.Query["id"]));

            UserActions.WriteReview(user, Book, text, value);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostDelay()
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=books/details?id={Request.Query["id"]}");

            Book = Repository.Get<Book>(book => book.Id == int.Parse(Request.Query["id"]));

            if (user.PendingBooks.Contains(Book))
                UserActions.UnDelay(user, Book);
            else
                UserActions.Delay(user, Book);

            return Redirect(Request.QueryString.Value);
        }
        
        public IActionResult OnPostEvaluate(int value)
        {
            var user = Auth.GetUser(HttpContext);
            
            if (user is null)
                return Redirect($"/login?from=books/details?id={Request.Query["id"]}");

            Book = Repository.Get<Book>(book => book.Id == int.Parse(Request.Query["id"]));

            UserActions.Evaluate(user, Book, value);

            return Redirect(Request.QueryString.Value);
        }
    }
}
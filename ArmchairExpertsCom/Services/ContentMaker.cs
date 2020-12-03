using System;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Services
{
    public static class ContentMaker
    {
        public static double GetRating(Book book)
        {
            return Repository
                .Filter<BookEvaluation>(be => be.Book.First() == book)
                .Select(be => be.Value)
                .Average();
        }
        
        public static double GetRating(Film film)
        {
            return Repository
                .Filter<FilmEvaluation>(fe => fe.Film.First() == film)
                .Select(fe => fe.Value)
                .Average();
        }
        
        public static double GetRating(Serial serial)
        {
            return Repository
                .Filter<SerialEvaluation>(se => se.Serial.First() == serial)
                .Select(se => se.Value)
                .Average();
        }
        
        public static IEnumerable<Book> GetSimilarBooks(Book book)
        {
            return Repository
                .Filter<Book>(b => b != book)
                .Select(b => (b, GetMatchingGenresCount(book, b)))
                .OrderByDescending(t => t.Item2)
                .Select(t => t.Item1)
                .Take(9);
        }
        
        public static IEnumerable<Film> GetSimilarFilms(Film film)
        {
            return Repository
                .Filter<Film>(f => f != film)
                .Select(f => (f, GetMatchingGenresCount(film, f)))
                .OrderByDescending(t => t.Item2)
                .Select(t => t.Item1)
                .Take(9);
        }
        
        public static IEnumerable<Serial> GetSimilarSerials(Serial serial)
        {
            return Repository
                .Filter<Serial>(s => s != serial)
                .Select(s => (s, GetMatchingGenresCount(serial, s)))
                .OrderByDescending(t => t.Item2)
                .Select(t => t.Item1)
                .Take(9);
        }

        private static int GetMatchingGenresCount(IArtwork artwork1, IArtwork artwork2)
        {
            return artwork1
                .Genres
                .Select(g => artwork2.Genres.Contains(g) ? 1 : 0)
                .Sum();
        }

        public static IEnumerable<Film> SearchFilms(string searchString)
        {
            return searchString is null ? Repository.All<Film>() : Repository
                .Filter<Film>(f => f.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                        f.Actors.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                        f.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                        f.Producers.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }
        
        public static IEnumerable<Serial> SearchSerials(string searchString)
        {
            return searchString is null ? Repository.All<Serial>() : Repository
                .Filter<Serial>(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                   s.Actors.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                   s.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                   s.Producers.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Book> SearchBooks(string searchString)
        {
            return searchString is null ? Repository.All<Book>() : Repository
                .Filter<Book>(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                   b.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                   b.Authors.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<User> GetSimilarUsers(User currentUser)
        {
            return Repository
                .Filter<User>(user => user != currentUser)
                .OrderBy(user => ClosenessPoints(user, currentUser))
                .Take(9);
        }

        private static int ClosenessPoints(User user1, User user2)
        {
            var result = 0;
            
            foreach (var evaluation in Repository.Filter<BookEvaluation>(e => e.User.First() == user1))
            {
                var sameEvaluation = Repository
                    .Get<BookEvaluation>(e => e.User.First() == user2 && e.Book == evaluation.Book);

                result += sameEvaluation is null ? 0 : 10 - Math.Abs(evaluation.Value - sameEvaluation.Value);
            }
            
            foreach (var evaluation in Repository.Filter<FilmEvaluation>(e => e.User.First() == user1))
            {
                var sameEvaluation = Repository
                    .Get<FilmEvaluation>(e => e.User.First() == user2 && e.Film == evaluation.Film);

                result += sameEvaluation is null ? 0 : 10 - Math.Abs(evaluation.Value - sameEvaluation.Value);
            }
            
            foreach (var evaluation in Repository.Filter<SerialEvaluation>(e => e.User.First() == user1))
            {
                var sameEvaluation = Repository
                    .Get<SerialEvaluation>(e => e.User.First() == user2 && e.Serial == evaluation.Serial);

                result += sameEvaluation is null ? 0 : 10 - Math.Abs(evaluation.Value - sameEvaluation.Value);
            }

            return result;
        }

        public static IEnumerable<IArtwork> GetMostPopular()
        {
            return Repository
                .All<Book>().Select(e => (IArtwork) e)
                .Concat(Repository.All<Film>())
                .Concat(Repository.All<Serial>())
                .OrderByDescending(GetPopularity)
                .Take(9);
        }

        private static double GetPopularity(IArtwork artwork)
        {
            return artwork switch
            {
                Book book => GetRating(book) *
                             Repository.Filter<BookEvaluation>(e => e.Book.First() == book).Count(),
                Film film => GetRating(film) *
                             Repository.Filter<FilmEvaluation>(e => e.Film.First() == film).Count(),
                Serial serial => GetRating(serial) *
                             Repository.Filter<SerialEvaluation>(e => e.Serial.First() == serial).Count(),
                _ => throw new ArgumentException()
            };
        }

        public static IEnumerable<User> SearchUsers(string searchString)
        {
            return searchString is null
                ? new User[] { }
                : Repository.Filter<User>(user =>
                    user.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    user.Login.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }
    }
}
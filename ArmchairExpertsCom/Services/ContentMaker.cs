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
            return Repository
                .Filter<Film>(f => f.Title.Contains(searchString) ||
                                        f.Actors.Contains(searchString) ||
                                        f.Description.Contains(searchString) ||
                                        f.Producers.Contains(searchString));
        }
        
        public static IEnumerable<Serial> SearchSerials(string searchString)
        {
            return Repository
                .Filter<Serial>(s => s.Title.Contains(searchString) ||
                                   s.Actors.Contains(searchString) ||
                                   s.Description.Contains(searchString) ||
                                   s.Producers.Contains(searchString));
        }

        public static IEnumerable<Book> SearchBooks(string searchString)
        {
            return Repository
                .Filter<Book>(b => b.Title.Contains(searchString) ||
                                   b.Description.Contains(searchString) ||
                                   b.Authors.Contains(searchString));
        }

        public static IEnumerable<IArtwork> SearchAll(string searchString)
        {
            foreach (var film in SearchFilms(searchString))
                yield return film;

            foreach (var serial in SearchSerials(searchString))
                yield return serial;

            foreach (var book in SearchBooks(searchString))
                yield return book;
        }
    }
}
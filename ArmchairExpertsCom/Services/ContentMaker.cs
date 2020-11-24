using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Services
{
    public static class ContentMaker
    {
        public static double GetBookRating(Book book)
        {
            return Repository
                .Filter<BookEvaluation>(be => be.Book.First() == book)
                .Select(be => be.Value)
                .Average();
        }
        
        public static double GetFilmRating(Film film)
        {
            return Repository
                .Filter<FilmEvaluation>(fe => fe.Film.First() == film)
                .Select(fe => fe.Value)
                .Average();
        }
        
        public static double GetSerialRating(Serial serial)
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
                .OrderBy(t => t.Item2)
                .Select(t => t.Item1)
                .Take(9);
        }
        
        public static IEnumerable<Film> GetSimilarFilms(Film film)
        {
            return Repository
                .Filter<Film>(f => f != film)
                .Select(f => (f, GetMatchingGenresCount(film, f)))
                .OrderBy(t => t.Item2)
                .Select(t => t.Item1)
                .Take(9);
        }
        
        public static IEnumerable<Serial> GetSimilarSerials(Serial serial)
        {
            return Repository
                .Filter<Serial>(s => s != serial)
                .Select(s => (s, GetMatchingGenresCount(serial, s)))
                .OrderBy(t => t.Item2)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Services
{
    public static class UserActions
    {
        public static void Evaluate(User user, Book book, int value)
        {
            if (value < 0 || value > 10)
                throw new ArgumentException();
            
            var evaluation = new BookEvaluation {Value = value};
            evaluation.Book.Add(book);
            evaluation.User.Add(user);
            evaluation.Save();

            if (user.PendingBooks.Contains(book))
                user.PendingBooks.Remove(book);

            Repository.SaveChanges();
        }
        
        public static void Evaluate(User user, Film film, int value)
        {
            if (value < 0 || value > 10)
                throw new ArgumentException();
            
            var evaluation = new FilmEvaluation {Value = value};
            evaluation.Film.Add(film);
            evaluation.User.Add(user);
            evaluation.Save();

            if (user.PendingFilms.Contains(film))
                user.PendingFilms.Remove(film);

            Repository.SaveChanges();
        }
        
        public static void Evaluate(User user, Serial serial, int value)
        {
            if (value < 0 || value > 10)
                throw new ArgumentException();
            
            var evaluation = new SerialEvaluation {Value = value};
            evaluation.Serial.Add(serial);
            evaluation.User.Add(user);
            evaluation.Save();

            if (user.PendingSerials.Contains(serial))
                user.PendingSerials.Remove(serial);

            Repository.SaveChanges();
        }

        public static void Delay(User user, IModel artwork)
        {
            switch (artwork)
            {
                case Book _:
                    user.PendingBooks.Add(artwork);
                    return;
                case Film _:
                    user.PendingFilms.Add(artwork);
                    return;
                case Serial _:
                    user.PendingSerials.Add(artwork);
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        public static void Subscribe(User subscriber, User subscribe)
        {
            if (subscriber.Subscribes.Contains(subscribe)) return;
            subscriber.Subscribes.Add(subscribe);
        }

        public static void UnSubscribe(User subscriber, User subscribe)
        {
            if (!subscriber.Subscribes.Contains(subscribe)) return;
            subscriber.Subscribes.Remove(subscribe);
        }

        public static void WriteReview(User user, Book book, string text, int value)
        {
            var review = new BookReview {Text = text};
            review.User.Add(user);
            review.Book.Add(book);
            review.Save();
            
            book.Reviews.Add(review);

            Evaluate(user, book, value);
            
            Repository.SaveChanges();
        }
        
        public static void WriteReview(User user, Film film, string text, int value)
        {
            var review = new FilmReview {Text = text};
            review.User.Add(user);
            review.Film.Add(film);
            review.Save();
            
            film.Reviews.Add(review);

            Evaluate(user, film, value);
            
            Repository.SaveChanges();
        }
        
        public static void WriteReview(User user, Serial serial, string text, int value)
        {
            var review = new SerialReview {Text = text};
            review.User.Add(user);
            review.Serial.Add(serial);
            review.Save();
            
            serial.Reviews.Add(review);

            Evaluate(user, serial, value);
            
            Repository.SaveChanges();
        }

        public static void WriteComment(User user, ICommentable content, string text)
        {
            var comment = new Comment {Text = text};
            comment.User.Add(user);
            comment.Save();
            
            content.Comments.Add(comment);
            
            Repository.SaveChanges();
        }

        public static void CreateSelection(User user, string title, string text)
        {
            var selection = new Selection {Title = title, Text = text};
            selection.User.Add(user);
            selection.Save();
            
            user.Selections.Add(selection);
        }

        public static void AddToSelection(User user, Selection selection, IArtwork artwork)
        {
            switch (artwork)
            {
                case Book book:
                    if (!selection.Books.Contains(book))
                        selection.Books.Add(book);
                    break;
                case Film film:
                    if (!selection.Films.Contains(film))
                        selection.Films.Add(film);
                    break;
                case Serial serial:
                    if (!selection.Serials.Contains(serial))
                        selection.Serials.Add(serial);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
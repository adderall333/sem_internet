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

            var previousEvaluation = Repository.Get<BookEvaluation>(e => e.Book.First() == book &&
                                                                         e.User.First() == user);
            previousEvaluation?.Book.Clear();
            previousEvaluation?.User.Clear();
            previousEvaluation?.Delete();


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
            
            var previousEvaluation = Repository.Get<FilmEvaluation>(e => e.Film.First() == film &&
                                                                         e.User.First() == user);
            previousEvaluation?.Film.Clear();
            previousEvaluation?.User.Clear();
            previousEvaluation?.Delete();
            
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
            
            var previousEvaluation = Repository.Get<SerialEvaluation>(e => e.Serial.First() == serial &&
                                                                         e.User.First() == user);
            previousEvaluation?.Serial.Clear();
            previousEvaluation?.User.Clear();
            previousEvaluation?.Delete();
            
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
                    Repository.SaveChanges();
                    return;
                case Film _:
                    user.PendingFilms.Add(artwork);
                    Repository.SaveChanges();
                    return;
                case Serial _:
                    user.PendingSerials.Add(artwork);
                    Repository.SaveChanges();
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        public static void UnDelay(User user, IModel artwork)
        {
            switch (artwork)
            {
                case Book _:
                    user.PendingBooks.Remove(artwork);
                    Repository.SaveChanges();
                    return;
                case Film _:
                    user.PendingFilms.Remove(artwork);
                    Repository.SaveChanges();
                    return;
                case Serial _:
                    user.PendingSerials.Remove(artwork);
                    Repository.SaveChanges();
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        public static void Subscribe(User subscriber, User subscribe)
        {
            if (subscriber.Subscribes.Contains(subscribe)) return;
            subscriber.Subscribes.Add(subscribe);
            Repository.SaveChanges();
        }

        public static void UnSubscribe(User subscriber, User subscribe)
        {
            if (!subscriber.Subscribes.Contains(subscribe)) return;
            subscriber.Subscribes.Remove(subscribe);
            Repository.SaveChanges();
        }

        public static void WriteReview(User user, Book book, string text, int value)
        {
            var review = new BookReview {Text = text};
            review.User.Add(user);
            review.Artwork.Add(book);
            review.Save();
            
            book.Reviews.Add(review);

            Evaluate(user, book, value);
            
            Repository.SaveChanges();
        }
        
        public static void WriteReview(User user, Film film, string text, int value)
        {
            var review = new FilmReview {Text = text};
            review.User.Add(user);
            review.Artwork.Add(film);
            review.Save();
            
            film.Reviews.Add(review);

            Evaluate(user, film, value);
            
            Repository.SaveChanges();
        }
        
        public static void WriteReview(User user, Serial serial, string text, int value)
        {
            var review = new SerialReview {Text = text};
            review.User.Add(user);
            review.Artwork.Add(serial);
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

        public static Selection CreateSelection(User user, string title, string text)
        {
            var selection = new Selection {Title = title, Text = text};
            selection.User.Add(user);
            selection.Save();
            
            user.Selections.Add(selection);
            Repository.SaveChanges();
            return selection;
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
            
            Repository.SaveChanges();
        }

        public static void DeleteProfile(User user)
        {
            foreach (var evaluation in Repository.Filter<BookEvaluation>(e => e.User.Contains(user)))
            {
                evaluation.Delete();
            }
            foreach (var evaluation in Repository.Filter<FilmEvaluation>(e => e.User.Contains(user)))
            {
                evaluation.Delete();
            }
            foreach (var evaluation in Repository.Filter<SerialEvaluation>(e => e.User.Contains(user)))
            {
                evaluation.Delete();
            }
            
            foreach (var review in Repository.Filter<BookReview>(e => e.User.Contains(user)))
            {
                review.Delete();
            }
            foreach (var review in Repository.Filter<FilmReview>(e => e.User.Contains(user)))
            {
                review.Delete();
            }
            foreach (var review in Repository.Filter<SerialReview>(e => e.User.Contains(user)))
            {
                review.Delete();
            }

            foreach (var comment in Repository.Filter<Comment>(e => e.User.Contains(user)))
            {
                comment.Delete();
            }
            foreach (var selection in Repository.Filter<Selection>(e => e.User.Contains(user)))
            {
                selection.Delete();
            }
            
            user.Delete();
            Repository.SaveChanges();
        }

        public static void ChangePrivacySettings(
            User user, 
            bool watched,
            bool subscribes,
            bool reviews,
            bool selections,
            bool pending)
        {
            var privacy = (PrivacySettings) user.Privacy.First();
            privacy.AreWatchedOpen = watched;
            privacy.AreSubscribesOpen = subscribes;
            privacy.AreReviewsOpen = reviews;
            privacy.AreSelectionsOpen = selections;
            privacy.ArePendingOpen = pending;
            
            privacy.Save();
            Repository.SaveChanges();
        }
    }
}
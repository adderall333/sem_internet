using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Book : IModel, IContent
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Book>($"title = {Title}, " +
                                             $"description = {Description}," +
                                             $"year = {Year}," +
                                             $"authors = {Authors}", Id);
            else
                ObjectsGetter.Insert<Book>("title, description, year, authors",
                    $"{Title}, {Description}, {Year}, {Authors}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Authors { get; set; }
        //image
        
        //from staging tables
        public IGenre[] Genres { get; set; }
        public int Rating { get; set; }
        public Review[] Reviews { get; set; }
        
    }
}
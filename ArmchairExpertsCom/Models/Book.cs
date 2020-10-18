using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Book : IModel, IContent
    {
        public int Id { get; set; }
        public bool IsInDataBase;

        public void FillIn(NpgsqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Title = reader.GetString(1);
            Description = reader.GetString(2);
            Year = reader.GetInt32(3);
            Authors = reader.GetString(4);
            IsInDataBase = true;
        }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update("book", $"title = {Title}, " +
                                             $"description = {Description}," +
                                             $"year = {Year}," +
                                             $"authors = {Authors}", Id);
            else
                ObjectsGetter.Insert("book", "title, description, year, authors",
                    $"{Title}, {Description}, {Year}, {Authors}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete("book", Id);
            IsInDataBase = false;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Authors { get; set; }
        public IGenre[] Genres { get; set; }
        public int Rating { get; set; }
        public Review[] Reviews { get; set; }
        //image
    }
}
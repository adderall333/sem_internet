using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Book : IModel, IContent
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Authors { get; set; }
        public Image Image { get; set; }
        
        //foreign keys etc.
        public IGenre[] Genres { get; set; }
        public int Rating { get; set; }
        public Review[] Reviews { get; set; }
    }
}
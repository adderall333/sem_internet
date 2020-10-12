using System;

namespace Sem_internet.Models
{
    public class Book : IModel, IContent
    {
        public int Id { get; set; }
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
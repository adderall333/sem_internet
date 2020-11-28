using System.Linq;
using System.Reflection;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class Book : IModel, IArtwork
    {
        public Book()
        {
            Images = new DbSet(this);
            Genres = new DbSet(this);
            Reviews = new DbSet(this);
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Authors { get; set; }
        
        
        [ForeignKey(typeof(Image))]
        public DbSet Images { get; private set; }
        
        [ForeignKey(typeof(BookGenre))]
        public DbSet Genres { get; private set; }
        
        [ForeignKey(typeof(BookReview))]
        public DbSet Reviews { get; private set; } 

        
        public void Save()
        {
            if (!Repository.Contains(this))
            {
                Repository.Add(this);
                IsNew = true;
            }
            else
                IsChanged = true;
        }
        
        public void Delete()
        {
            IsDeleted = true;
        }

        public override string ToString()
        {
            return $"{Id}.{Title} ({Authors})";
        }
    }
}
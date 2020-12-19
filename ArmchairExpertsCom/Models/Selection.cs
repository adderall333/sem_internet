using System.Linq;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class Selection : IModel, ICommentable
    {
        public Selection()
        {
            User = new DbSet(this);
            Books = new DbSet(this);
            Films = new DbSet(this);
            Serials = new DbSet(this);
            Comments = new DbSet(this);
        }
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; private set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet Books { get; private set; }
        
        [ForeignKey(typeof(Film))]
        public DbSet Films { get; private set; }
        
        [ForeignKey(typeof(Serial))]
        public DbSet Serials { get; private set; }

        [ForeignKey(typeof(Comment))]
        public DbSet Comments { get; private set; }
        
        
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
            Books.Clear();
            Films.Clear();
            Serials.Clear();
            Comments.Clear();
            IsDeleted = true;
        }

        public IArtwork[] GetArtworks()
        {
            return Books
                .Concat(Films)
                .Concat(Serials)
                .Select(e => (IArtwork) e)
                .ToArray();
        }
    }
}
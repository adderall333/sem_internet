using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class FilmReview : IModel, IReview, ICommentable
    {
        public FilmReview()
        {
            User = new DbSet(this);
            Film = new DbSet(this);
            Comments = new DbSet(this);
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }

        
        public int Id { get; set; }
        public string Text { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; private set; }
        
        [ForeignKey(typeof(Film))]
        public DbSet Film { get; private set; }

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
            IsDeleted = true;
        }
    }
}
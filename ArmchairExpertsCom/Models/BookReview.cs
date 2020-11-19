using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class BookReview : IModel
    {
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }

        
        public int Id { get; set; }
        public string Text { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet Book { get; set; }

        [ForeignKey(typeof(Comment))]
        public DbSet Comments { get; set; }
        
        
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
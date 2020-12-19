using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class BookEvaluation : IModel, IEvaluation
    {
        public BookEvaluation()
        {
            User = new DbSet(this);
            Book = new DbSet(this);
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public int Value { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; private set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet Book { get; private set; }
        
        
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
            Book.Clear();
            IsDeleted = true;
        }
    }
}
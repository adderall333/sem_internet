using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class User : IModel
    {
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string PasswordKey { get; set; }
        public string BirthDate { get; set; }
        public string Role { get; set; }
        
        
        [ForeignKey(typeof(Image))]
        public DbSet Images { get; set; }
        
        [ForeignKey(typeof(User))]
        public DbSet Subscribes { get; set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet ReadBooks { get; set; }

        [ForeignKey(typeof(Film))]
        public DbSet WatchedFilms { get; set; }
        
        [ForeignKey(typeof(Serial))]
        public DbSet WatchedSerials { get; set; }
        
        [ForeignKey(typeof(Comment))]
        public DbSet Comments { get; set; }
        
        [ForeignKey(typeof(Selection))]
        public DbSet Selections { get; set; }
        
        
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
            return $"{Id}.{FullName}";
        }
    }
}
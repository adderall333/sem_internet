using ArmchairExpertsCom.Pages.Models.Interfaces;
using ArmchairExpertsCom.Pages.Models.Utilities;
using Npgsql;

namespace ArmchairExpertsCom.Pages.Models
{
    public class Selection : IModel
    {
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string Title { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet Books { get; set; }
        
        [ForeignKey(typeof(Film))]
        public DbSet Films { get; set; }
        
        [ForeignKey(typeof(Serial))]
        public DbSet Serials { get; set; }

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
using ArmchairExpertsCom.Pages.Models.Interfaces;
using ArmchairExpertsCom.Pages.Models.Utilities;
using Npgsql;

namespace ArmchairExpertsCom.Pages.Models
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
        public string PasswordHash { get; set; }
        public string RegistrationDate { get; set; }
        
        
        [ForeignKey(typeof(Image))]
        public DbSet Image { get; set; }
        
        [ForeignKey(typeof(User))]
        public DbSet Friends { get; set; }
        
        [ForeignKey(typeof(Selection))]
        public DbSet Selections { get; set; }

        [ForeignKey(typeof(Review))]
        public DbSet Reviews { get; set; }
        
        [ForeignKey(typeof(BookEvaluation))]
        public DbSet BookEvaluations { get; set; }
        
        [ForeignKey(typeof(FilmEvaluation))]
        public DbSet FilmEvaluations { get; set; }
        
        [ForeignKey(typeof(SerialEvaluation))]
        public DbSet SerialEvaluations { get; set; }
        
        [ForeignKey(typeof(Film))]
        public DbSet WatchedFilms { get; set; }
        
        [ForeignKey(typeof(Serial))]
        public DbSet WatchedSerials { get; set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet ReadBooks { get; set; }
        
        
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
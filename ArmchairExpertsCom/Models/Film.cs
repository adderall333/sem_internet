using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class Film : IModel, IArtwork
    {
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
        public string Producers { get; set; }
        public string Actors { get; set; }
        
        
        [ForeignKey(typeof(FilmReview))]
        public DbSet Reviews { get; set; }
        
        [ForeignKey(typeof(FilmGenre))]
        public DbSet Genres { get; set; }
        
        [ForeignKey(typeof(Image))]
        public DbSet Images { get; set; }
        
        
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
            return $"{Id}.{Title} ({Year})";
        }
    }
}
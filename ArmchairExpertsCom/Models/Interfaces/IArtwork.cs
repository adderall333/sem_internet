using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface IArtwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DbSet Images { get; set; }
        public DbSet Genres { get; set; } 
        public DbSet Reviews { get; set; }
    }
}
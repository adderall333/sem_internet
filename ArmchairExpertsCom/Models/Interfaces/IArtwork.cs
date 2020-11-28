using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface IArtwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DbSet Images { get; }
        public DbSet Genres { get; } 
        public DbSet Reviews { get; }
    }
}
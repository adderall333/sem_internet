using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface IReview : IModel
    {
        public string Text { get; set; }
        public DbSet Artwork { get; set; }
        public DbSet User { get; }
        public DbSet Comments { get; }
    }
}
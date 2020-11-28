using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface ICommentable
    {
        public DbSet Comments { get; }
    }
}
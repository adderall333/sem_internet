using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface IEvaluation
    {
        public int Value { get; set; }
        public DbSet User { get; set; }
    }
}
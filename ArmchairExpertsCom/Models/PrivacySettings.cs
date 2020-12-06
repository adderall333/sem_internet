using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class PrivacySettings : IModel
    {
        public PrivacySettings()
        {
            User = new DbSet(this);
            AreWatchedOpen = true;
            AreSubscribesOpen = true;
            AreReviewsOpen = true;
            AreSelectionsOpen = true;
            ArePendingOpen = true;
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public bool AreWatchedOpen { get; set; }
        public bool AreSubscribesOpen { get; set; }
        public bool AreReviewsOpen { get; set; }
        public bool AreSelectionsOpen { get; set; }
        public bool ArePendingOpen { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; private set; }
        
        
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
using System;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Recommendation : IModel, IWriting
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        //foreign keys etc.
        public User User { get; set; }
        public IContent RecommendationTarget { get; set; }
        public Comment[] Comments { get; set; }
    }
}
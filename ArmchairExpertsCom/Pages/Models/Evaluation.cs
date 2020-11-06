using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Evaluation : IModel
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }
        public int Value { get; set; }
        
        //foreign keys etc. 
        public User User { get; set; }
        public IContent Content { get; set; }
    }
}
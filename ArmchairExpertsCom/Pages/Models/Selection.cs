using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Selection : IModel
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }
        public string Title { get; set; }
        
        //foreign keys etc.
        public User User { get; set; }
        public IContent[] Contents { get; set; }
        public Comment[] Comments { get; set; }
    }
}
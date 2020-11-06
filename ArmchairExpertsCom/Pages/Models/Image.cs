namespace ArmchairExpertsCom.Pages
{
    public class Image : IModel
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }
        public string Path { get; set; }
        
        //foreign keys etc.
    }
}
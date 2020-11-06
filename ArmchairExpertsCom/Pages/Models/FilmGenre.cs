using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class FilmGenre : IModel, IGenre
    {
        //basic properties
        public int Id { get; set; }
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }
        public string Name { get; set; }
        
        //foreign keys etc.
        public IContent[] Contents { get; set; }
    }
}
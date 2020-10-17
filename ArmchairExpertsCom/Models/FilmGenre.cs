namespace ArmchairExpertsCom.Models
{
    public class FilmGenre : IModel, IGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IContent[] Contents { get; set; }
    }
}
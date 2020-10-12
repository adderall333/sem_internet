namespace Sem_internet.Models
{
    public interface IContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IGenre[] Genres { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public Review[] Reviews { get; set; }
    }
}
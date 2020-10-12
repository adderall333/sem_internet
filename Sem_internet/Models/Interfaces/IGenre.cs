namespace Sem_internet.Models
{
    public interface IGenre
    {
        public string Name { get; set; }
        public IContent[] Contents { get; set; }
    }
}
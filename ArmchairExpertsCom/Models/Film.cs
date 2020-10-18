using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Film : IModel, IContent
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public IGenre[] Genres { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Cast { get; set; }
        public Review[] Reviews { get; set; }
        //images
    }
}
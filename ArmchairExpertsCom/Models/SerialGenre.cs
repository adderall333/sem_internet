using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class SerialGenre : IModel
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        public string Name { get; set; }
        public Serial Serials { get; set; }
    }
}
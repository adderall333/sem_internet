using System.Collections.Generic;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class BookGenre : IModel, IGenre
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        public string Name { get; set; }
        public IContent[] Contents { get; set; }
    }
}
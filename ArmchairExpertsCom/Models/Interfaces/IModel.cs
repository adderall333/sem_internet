using System.Collections;
using System.Collections.Generic;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public interface IModel
    {
        public int Id { get; set; }

        public void FillIn(NpgsqlDataReader reader);
    }
}
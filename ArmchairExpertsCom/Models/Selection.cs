﻿using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Selection : IModel
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        public int UserId { get; set; }
        public string Title { get; set; }
        public IContent[] Contents { get; set; }
        public Comment[] Comments { get; set; }
    }
}
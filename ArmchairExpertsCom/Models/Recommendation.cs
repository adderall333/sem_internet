using System;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Recommendation : IModel, IWriting
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public IContent RecommendationTarget { get; set; }
        public Comment[] Comments { get; set; }
    }
}
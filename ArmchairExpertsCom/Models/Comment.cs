using System;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Comment : IModel, IWriting
    {
        public int Id { get; set; }
        public void FillIn(NpgsqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public IWriting CommentTarget { get; set; }
        public Comment[] Comments { get; set; }
    }
}
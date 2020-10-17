using System;

namespace ArmchairExpertsCom.Models
{
    public class Review : IModel, IWriting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public IContent ReviewTarget { get; set; }
        public Comment[] Comments { get; set; }
    }
}
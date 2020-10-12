using System;

namespace Sem_internet.Models
{
    public interface IWriting
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Comment[] Comments { get; set; }
    }
}
using System;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Comment : IModel, IWriting
    {
        //basic properties
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        //foreign keys etc.
        public User User { get; set; }
        public IWriting CommentTarget { get; set; }
        public Comment[] Comments { get; set; }
        
        //methods etc.
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Comment>($"text = {Text}, " + 
                                              $"date = {Date}, ", Id);
            else
                ObjectsGetter.Insert<Comment>("text, date", $"{Text}, {Date}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Comment>(Id);
            IsInDataBase = false;
        }
    }
}
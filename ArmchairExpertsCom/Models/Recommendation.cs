using System;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Recommendation : IModel, IWriting
    {
        //basic properties
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        //foreign keys etc.
        public User User { get; set; }
        public IContent RecommendationTarget { get; set; }
        public Comment[] Comments { get; set; }
        
        //methods etc.
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Recommendation>($"text = {Text}, " + 
                                                     $"date = {Date}", Id);
            else
                ObjectsGetter.Insert<Recommendation>("text, date", $"{Text}, {Date}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Recommendation>(Id);
            IsInDataBase = false;
        }
    }
}
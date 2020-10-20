using System;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Recommendation : IModel, IWriting
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Recommendation>($"text = {Text}" + 
                                                     $"date = {Date}", Id);
            else
                ObjectsGetter.Insert<Recommendation>("text, data", $"{Text}, {Date}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }
        
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        //from staging tables
        public User User { get; set; }
        public IContent RecommendationTarget { get; set; }
        public Comment[] Comments { get; set; }
    }
}
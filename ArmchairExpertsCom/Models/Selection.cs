using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Selection : IModel
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Selection>($"title = {Title}", Id);
            else
                ObjectsGetter.Insert<Selection>("title", $"{Title}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }
        
        public string Title { get; set; }
        
        //from staging tables
        public User User { get; set; }
        public IContent[] Contents { get; set; }
        public Comment[] Comments { get; set; }
    }
}
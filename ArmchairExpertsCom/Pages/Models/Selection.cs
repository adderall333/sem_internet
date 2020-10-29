using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Selection : IModel
    {
        //basic properties
        public int Id { get; set; }
        public string Title { get; set; }
        
        //foreign keys etc.
        public User User { get; set; }
        public IContent[] Contents { get; set; }
        public Comment[] Comments { get; set; }
        
        //methods etc.
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
            ObjectsGetter.Delete<Selection>(Id);
            IsInDataBase = false;
        }
    }
}
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class SerialGenre : IModel, IGenre
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<SerialGenre>($"name = {Name}", Id);
            else
                ObjectsGetter.Insert<SerialGenre>("name", $"{Name}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<SerialGenre>(Id);
            IsInDataBase = false;
        }

        public string Name { get; set; }
        
        //from staging tables
        public IContent[] Contents { get; set; }
    }
}
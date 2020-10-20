using System.Runtime.Serialization;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class SerialGenre : IModel, IGenre
    {
        //basic properties
        public int Id { get; set; }
        public string Name { get; set; }
        
        //foreign keys etc.
        public IContent[] Contents { get; set; }
        
        //methods etc.
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
    }
}
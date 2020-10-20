using System.Collections.Generic;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class BookGenre : IModel, IGenre
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
                ObjectsGetter.Update<BookGenre>($"name = {Name}", Id);
            else
                ObjectsGetter.Insert<BookGenre>("name", $"{Name}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<BookGenre>(Id);
            IsInDataBase = false;
        }
    }
}
using System.Collections.Generic;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class BookGenre : IModel, IGenre
    {
        public int Id { get; set; }
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
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }

        public string Name { get; set; }
        
        //from staging tables
        public IContent[] Contents { get; set; }
    }
}
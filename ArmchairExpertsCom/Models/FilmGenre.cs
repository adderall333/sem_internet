using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class FilmGenre : IModel, IGenre
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<FilmGenre>($"name = {Name}", Id);
            else
                ObjectsGetter.Insert<FilmGenre>("name", "{Name}");
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
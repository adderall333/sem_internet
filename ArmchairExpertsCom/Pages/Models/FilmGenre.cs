using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class FilmGenre : IModel, IGenre
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
                ObjectsGetter.Update<FilmGenre>($"name = {Name}", Id);
            else
                ObjectsGetter.Insert<FilmGenre>("name", "{Name}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<FilmGenre>(Id);
            IsInDataBase = false;
        }
    }
}
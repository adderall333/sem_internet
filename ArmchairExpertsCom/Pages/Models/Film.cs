using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Film : IModel, IContent
    {
        //basic properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Cast { get; set; }
        
        //foreign keys etc.
        public IGenre[] Genres { get; set; }
        public Review[] Reviews { get; set; }
        public Image[] Images { get; set; }
        
        //methods etc.
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if  (IsInDataBase)
                ObjectsGetter.Update<Film>($"title = {Title}, " + 
                                           $"description = {Description}, " +
                                           $"rating = {Rating}, " + 
                                           $"year = {Year}, " + 
                                           $"cast = {Cast}", Id);
            else
                ObjectsGetter.Insert<Film>("title, description, rating, year, cast", 
                    $"{Title}, {Description}, {Rating}, {Year}, {Cast}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Film>(Id);
            IsInDataBase = false;
        }
    }
}
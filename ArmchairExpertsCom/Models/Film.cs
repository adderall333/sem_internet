using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Film : IModel, IContent
    {
        public int Id { get; set; }
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
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Cast { get; set; }
        
        //from staging tables
        public IGenre[] Genres { get; set; }
        public Review[] Reviews { get; set; }
        //images
    }
}
namespace Sem_internet.Models
{
    public class User : IModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string RegistrationDate { get; set; }
        //avatar
        public Recommendation Favourites { get; set; }
        public User[] Friends { get; set; }
        public string Settings { get; set; }
        public Selection[] Selections { get; set; }
        public Comment[] Comments { get; set; }
        public Review[] Reviews { get; set; }
        public Recommendation[] Recommendations { get; set; }
        public Evaluation[] Evaluations { get; set; }
        public Film[] WatchedFilms { get; set; }
        public Serial[] WatchedSerials { get; set; }
        public Book[] ReadBooks { get; set; }
    }
}
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class User : IModel
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<User>($"full_name = {FullName}, " +
                                           $"login = {Login}, " +
                                           $"password_hash = {PasswordHash}, " +
                                           $"registration_date = {RegistrationDate}", Id);
            else
                ObjectsGetter.Insert<User>("full_name, login, password_hash, registration_date", 
                    $"{FullName}, {Login}, {PasswordHash}, {RegistrationDate}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<User>(Id);
            IsInDataBase = false;
        }

        public string FullName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string RegistrationDate { get; set; }

        //from staging tables
        //image
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
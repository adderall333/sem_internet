using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public class Evaluation : IModel
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }

        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Evaluation>($"value = {Value}", Id);
            else
                ObjectsGetter.Insert<Evaluation>("value", $"{Value}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Book>(Id);
            IsInDataBase = false;
        }

        public int Value { get; set; }
        
        //from staging tables
        public User User { get; set; }
        public IContent Content { get; set; }
    }
}
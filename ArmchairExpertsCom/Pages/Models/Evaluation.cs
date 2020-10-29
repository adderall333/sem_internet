using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public class Evaluation : IModel
    {
        //basic properties
        public int Id { get; set; }
        public int Value { get; set; }
        
        //foreign keys etc. 
        public User User { get; set; }
        public IContent Content { get; set; }
        
        //methods etc.
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
            ObjectsGetter.Delete<Evaluation>(Id);
            IsInDataBase = false;
        }
    }
}
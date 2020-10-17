namespace ArmchairExpertsCom.Models
{
    public class Evaluation : IModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public User User { get; set; }
        public IContent Content { get; set; }
    }
}
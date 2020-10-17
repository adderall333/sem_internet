namespace ArmchairExpertsCom.Models
{
    public class SerialGenre : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Serial Serials { get; set; }
    }
}
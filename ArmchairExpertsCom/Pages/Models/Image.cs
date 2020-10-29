namespace ArmchairExpertsCom.Pages
{
    public class Image : IModel
    {
        //basic properties
        public int Id { get; set; }
        public string Path { get; set; }
        
        //foreign keys etc.
        
        //methods etc.
        public bool IsInDataBase { get; set; }
        
        public void Save()
        {
            if (IsInDataBase)
                ObjectsGetter.Update<Image>($"path = {Path}", Id);
            else
                ObjectsGetter.Insert<Image>("path", $"{Path}");
            IsInDataBase = true;
        }

        public void Delete()
        {
            ObjectsGetter.Delete<Image>(Id);
            IsInDataBase = false;
        }
    }
}
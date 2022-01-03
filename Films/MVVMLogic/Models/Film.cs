namespace Films.MVVMLogic.Models
{
    public class Film
    {
        public string Name { get; set; }
        public string Picture { get; set; }

        public Film()
        {
        }

        public Film(string name, string picture)
        {
            Name = name;
            Picture = picture;
        }
    }
}

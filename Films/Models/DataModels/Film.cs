namespace Films.Models.DataModels
{
    public class Film
    {
        public Film()
        {}

        public Film(string name, string picture)
        {
            Name = name;
            Picture = picture;
        }
        
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}

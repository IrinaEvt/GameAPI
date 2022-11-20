namespace Models
{
    public class Genre
    {
        Genre()
        {
            Games = new List<Game>();
        }

        public Genre(string name):base()
        {
            Name = name;
        }

        public Genre(string name, List<Game> games) : base()
        {
            Name = name;
            Games = games;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}
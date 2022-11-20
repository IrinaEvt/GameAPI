using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Tag
    {
        Tag()
        {
            Games = new List<Game>();
        }

        public Tag(string name) : base()
        {
            Name = name;
        }

        public Tag(string name, List<Game> games) : base()
        {
            Name = name;
            Games = games;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
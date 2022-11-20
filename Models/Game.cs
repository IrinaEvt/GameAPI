using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Models
{
    public class Game
    {
        Game()
        {
            Tags = new List<Tag>();
        }

        public Game(string name, Price price, int genreId,Genre genre,  List<Tag> tags) : base()
        {
            Name = name;
            Price = price;
            GenreId = genreId;
            Genre = genre;
            Tags = tags;
        }

        public int Id { get; set; }

        public string Name { get; set; }
            
        public Price Price { get; set; }       

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public List<Tag> Tags { get; set; }

        public void SetPrice(Game game, Price price)
        {
            game.Price.Amount = price.Amount;
            game.Price.Currency = price.Currency;
        }
    }
}
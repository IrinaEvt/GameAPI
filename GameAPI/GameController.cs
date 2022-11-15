using Microsoft.AspNetCore.Mvc;
using Models;
using Mapping;
using System.Globalization;


namespace GameAPI
{
    [Route("games")]
    [ApiController]
    public class GameController : ControllerBase
    {

        IEnumerable<string> currencySymbols = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                .Distinct()
                .OrderBy(x => x);

        private ApplicationDBContext _dbContext;

        public GameController(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetGame")]
        public IActionResult getGame([FromBody] GetGameRequest request)
        {
            var game = _dbContext.Games.First(x => x.Id == request.GameId);

            var gameResponse = new GetGameResponse();

            gameResponse.Id = game.Id;
            gameResponse.Name = game.Name;
            gameResponse.Price = game.Price;
            gameResponse.Currency = game.Currency;
            gameResponse.GenreId = game.GenreId;
            gameResponse.GenreName = _dbContext.Genres.Where(g => g.Id == game.GenreId).First().Name;
            gameResponse.Tags = new List<TagResponse>();

            var tags = _dbContext.Tags.Where(t => t.Games.Contains(game)).ToList();
            foreach (var tag in tags)
            {
                var tagResponse = new TagResponse();
                tagResponse.Id = tag.Id;
                tagResponse.Name = tag.Name;

                gameResponse.Tags.Add(tagResponse);
            }
            return Ok(gameResponse);
        }

        public class GetGameRequest
        {
            public int GameId { get; set; }
        }

        public class TagResponse
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class GetGameResponse
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Price { get; set; }
            public string Currency { get; set; }

            public int GenreId { get; set; }

            public string GenreName { get; set; }

            public List<TagResponse> Tags { get; set; }
        }

        [HttpPost]
        [Route("SetGame")]
        public async Task<IActionResult> setGameAsync([FromBody] SetGameRequest request)
        {
            var Genre = await _dbContext.Genres.FindAsync(request.GenreId);
            var tags = new List<Tag>();
            foreach (int tagId in request.TagIds)
            {
                var Tag = await _dbContext.Tags.FindAsync(tagId);
                tags.Add(Tag);
            }



            var newGame = new Game
            {
                Name = request.Name,
                Price = request.Price ,
                Currency = request.Currency ,
                GenreId = request.GenreId,
                Genre = Genre,
                Tags = tags,
            };



            await _dbContext.Games.AddAsync(newGame);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }


        public class SetGameRequest
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            public string Currency { get; set; }
            public int GenreId { get; set; }

            public List<int> TagIds
            { get; set; }


        }

        /*
        [HttpGet("{id?}")]
        public IActionResult GetGameByID(int? id)
        {
            var game = dbContext.Games.Where(x => x.Id == id).FirstOrDefault();
            return Ok(game);
        }
        */


        [HttpGet]
        [Route("GetGameById")]
        public IActionResult GetGameByID(int id)
        {
            var game = _dbContext.Games.Where(x => x.Id == id).FirstOrDefault();
            return Ok(game);
        }


    

        [HttpGet]
        [Route("GetGames")]
        public async Task<ActionResult> Get()
        {
            var games = _dbContext.Games.ToList();
            return Ok(games);

        }

        [HttpPost]
        [Route("SetPrice")]
        public async Task<ActionResult> SetPrice([FromBody] SetPriceRequest request)
        {
            var game = _dbContext.Games.Find(request.GameId);
            game.Price = request.Price;
            game.Currency = request.Currency;


            await _dbContext.SaveChangesAsync();
            return Ok(game);

        }


        public class SetPriceRequest
        {
            public int GameId { get; set; }
            public decimal Price { get; set; }

            public string Currency { get; set; }

        }

    }
}

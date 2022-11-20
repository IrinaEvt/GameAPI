using Microsoft.AspNetCore.Mvc;
using Models;
using Mapping;
using GameAPI.Services;
using System.ComponentModel.DataAnnotations;


namespace GameAPI
{
    [Route("games")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private ApplicationDBContext _dbContext;
        private GameService gameService;

        public GameController(ApplicationDBContext dbContext, GameService _gameService)
        {
            this._dbContext = dbContext;
            this.gameService = _gameService;
        }



        [HttpGet]
        [Route("GetGameById")]
        public async Task<IActionResult> GetGameById([FromQuery] GetGameRequest request)
        {

            GameService.GetGameResponse gameResponse = await gameService.getGameById();

            if (gameResponse is null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(gameResponse);
            }
        }


        public class GetGameRequest
        {
            public int GameId { get; set; }

        }


        [HttpGet]
        [Route("GetGames")]
        public async Task<ActionResult> GetGames()
        {
            List<GameService.GetGameResponse> games = await gameService.getListOfGames();
            if (games.Count > 0)
            {
                return Ok(games);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("SetGame")]
        public async Task<IActionResult> setGameAsync([FromBody] SetGameRequest request)
        {

            gameService.SetGame(request);
            return Ok();

        }

        [HttpPost]
        [Route("SetPrice")]
        public async Task<ActionResult> SetPrice([FromBody] SetPriceRequest request)
        {

            gameService.SetPrice(request);
            return Ok();

        }


        public class SetGameRequest
        {

            SetGameRequest()
            {
                TagIds = new List<int>();
            }

            public string Name { get; set; }

            public Price Price { get; set; }

            public int GenreId { get; set; }

            public List<int> TagIds { get; set; }

        }

        public class SetPriceRequest
        {
            [Required]
            public int? GameId { get; set; }

            [Required]
            public Price Price { get; set; }

        }

        /*
        [HttpGet("{id?}")]
        public IActionResult GetGameByID(int? id)
        {
            var game = dbContext.Games.Where(x => x.Id == id).FirstOrDefault();
            return Ok(game);
        }
        */

        /*
        [HttpGet]
        [Route("GetGameById")]
        public IActionResult GetGameByID(int id)
        {
            var game = _dbContext.Games.Where(x => x.Id == id).FirstOrDefault();
            return Ok(game);
        }
        */

    }
}

using Mapping;
using Models;

namespace GameAPI.Services
{
    public class GameService
    {
        private ApplicationDBContext _dbContext;

        public GameService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetGameResponse>> getListOfGames()
        {
            List<Game> games = _dbContext.Games.ToList();

            List<GetGameResponse> responses = new List<GetGameResponse>();

            foreach (var game in games)
            {
                var gameResponse = new GetGameResponse(
                    game.Id,
                    game.Name,
                    game.Price,
                    game.GenreId,
                    //GenreName
                    _dbContext.Genres.Where(g => g.Id == game.GenreId).First().Name,
                    //tags
                    new List<TagResponse>()
                    );
                responses.Add(gameResponse);

                var tags = _dbContext.Tags.Where(t => t.Games.Contains(game)).ToList();

                foreach (var tag in tags)
                {
                    var tagResponse = new TagResponse();

                    tagResponse.Id = tag.Id;
                    tagResponse.Name = tag.Name;

                    gameResponse.Tags.Add(tagResponse);
                }

            }
            return responses;
        }

        public async Task<GetGameResponse> getGameById()
        {
            GameController.GetGameRequest request = new GameController.GetGameRequest();

            var game = _dbContext.Games.First(x => x.Id == request.GameId);
            var GenreName = _dbContext.Genres.Where(g => g.Id == game.GenreId).First().Name;

            List<TagResponse> tagsResponses = new List<TagResponse>();

            var gameResponse = new GetGameResponse(
                game.Id,
                game.Name,
                game.Price,
                game.GenreId,
                //GenreName
                GenreName,
                //tags
                tagsResponses
                );


            var tags = _dbContext.Tags.Where(t => t.Games.Contains(game)).ToList();

            foreach (var tag in tags)
            {
                var tagResponse = new TagResponse();

                tagResponse.Id = tag.Id;
                tagResponse.Name = tag.Name;

                gameResponse.Tags.Add(tagResponse);
            }

            return gameResponse;

        }


        public async void SetGame(GameController.SetGameRequest request)
        {
            var Genre = await _dbContext.Genres.FindAsync(request.GenreId);

            var tags = new List<Tag>();

            foreach (int tagId in request.TagIds)
            {
                var Tag = await _dbContext.Tags.FindAsync(tagId);
                tags.Add(Tag);
            }


            var newGame = new Game
            (
                request.Name,
                request.Price,
                request.GenreId,
                Genre,
                tags
            );


            await _dbContext.Games.AddAsync(newGame);
            await _dbContext.SaveChangesAsync();
            
        }


        public async void SetPrice(GameController.SetPriceRequest request)
        {
            var game = _dbContext.Games.Find(request.GameId);
            game.Price = request.Price;


            await _dbContext.SaveChangesAsync();
            
        }



        public class GetGameResponse
        {

            GetGameResponse()
            {
                Tags = new List<TagResponse>();
            }

            public GetGameResponse(int id, string name, Price price, int genreId, string genreName, List<TagResponse> tags) : base()
            {
                Id = id;
                Name = name;
                Price = price;
                GenreId = genreId;
                GenreName = genreName;
                Tags = tags;
            }

            public int Id { get; set; }

            public string Name { get; set; }

            public Price Price { get; set; }

            public int GenreId { get; set; }

            public string GenreName { get; set; }

            public List<TagResponse> Tags { get; set; }
        }

        public class TagResponse
        {
            public int Id { get; set; }

            public string Name { get; set; }

        }

    }
}

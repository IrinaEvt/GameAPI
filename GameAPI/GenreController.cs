
using Microsoft.AspNetCore.Mvc;
using Mapping;
using Models;


namespace GameAPI
{

    [Route("genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private ApplicationDBContext dbContext;

        public GenreController(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        

        [HttpPost]
        [Route("SetGenre")]
        public async Task<IActionResult> setGenreAsync([FromBody] SetGenreRequest request)
        {
            var newGenre = new Genre
            {
                Name = request.Name,
            };

            Console.WriteLine("TEEEEESSTTTT");
            await dbContext.Genres.AddAsync(newGenre);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        public class SetGenreRequest
        {
            public string Name { get; set; }

        }
    }
}

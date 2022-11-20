
using Microsoft.AspNetCore.Mvc;
using Mapping;
using Models;
using GameAPI.Services;

namespace GameAPI
{

    [Route("genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private GenreService genreService;

        public GenreController(GenreService _genreService)
        {
            this.genreService = _genreService;
        }


        [HttpPost]
        [Route("SetGenre")]
        public async Task<IActionResult> setGenreAsync([FromBody] SetGenreRequest request)
        {
            await genreService.SetGenre(request.Name);

            return Ok();
        }


        public class SetGenreRequest
        {
            public string Name { get; set; }

        }
    }
}

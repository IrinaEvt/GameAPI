using Mapping;
using Models;

namespace GameAPI.Services
{
    public class GenreService
    {

        private ApplicationDBContext _dbContext;

        public GenreService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SetGenre(string name)
        {
            var newGenre = new Genre(name);
            

            await _dbContext.Genres.AddAsync(newGenre);
            await _dbContext.SaveChangesAsync();

        }

    }
}

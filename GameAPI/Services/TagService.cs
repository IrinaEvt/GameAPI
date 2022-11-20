using Mapping;
using Models;

namespace GameAPI.Services
{
    public class TagService
    {
        private ApplicationDBContext dbContext;
        public TagService(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task SetTag(string name)
        {
            var newTag = new Tag(name);
           

            await dbContext.Tags.AddAsync(newTag);
            await dbContext.SaveChangesAsync();
        }
    }
}

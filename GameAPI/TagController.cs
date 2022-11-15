using Microsoft.AspNetCore.Mvc;

using Models;
using Mapping;

namespace GameAPI
{


    [Route("tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private ApplicationDBContext dbContext;
        public TagController(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }


        [HttpPost]
        [Route("SetTag")]
        public async Task<IActionResult> setTagAsync([FromBody] SetTagRequest request)
        {
            var newTag = new Tag
            {
                Name = request.Name,
            };

            await dbContext.Tags.AddAsync(newTag);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        public class SetTagRequest
        {
            public string Name { get; set; }
        }


    }
}

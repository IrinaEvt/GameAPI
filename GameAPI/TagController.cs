using Microsoft.AspNetCore.Mvc;

using Models;
using Mapping;
using GameAPI.Services;

namespace GameAPI
{


    [Route("tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private TagService tagService;

        public TagController(TagService _tagService)
        {
            this.tagService = _tagService;
        }


        [HttpPost]
        [Route("SetTag")]
        public async Task<IActionResult> setTagAsync([FromBody] SetTagRequest request)
        {
            await tagService.SetTag(request.Name);
            return Ok();

        }


        public class SetTagRequest
        {
            public string Name { get; set; }
        }


    }
}

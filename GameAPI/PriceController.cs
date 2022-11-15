using Microsoft.AspNetCore.Mvc;
using System.Net;
using Models;

namespace GameAPI
{
    [Route("price")]
    [ApiController]
    public class PriceController : ControllerBase
    {

        

        [HttpPost]
        [Route("SetPrice")]
        public async Task<IActionResult> setPriceAsync([FromBody] SetPriceRequest request)
        {
            return Ok();
        }

        public class SetPriceRequest
        {
            public string GameId { get; set; }

                      
        }

    }
}

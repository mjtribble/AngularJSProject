using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;


namespace _491Project.Controllers.API
{
    public class ItemController :ApiController
    {
        [HttpGet]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetItem(int id)
        {
            int result = id * 10;
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<>))]
        public async Task<IHttpActionResult> GetItemList()
        {
            List<string> list = new List<string>
            {
                "Hi", "Hello.", "Hi", "Hello", "How", "are", "you?"
            };
            return Ok(list);
        }

        [HttpPost]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> CreateItem([FromBody] String payload)
        {//testing comment
            return CreatedAtRoute(
                "DefaultApi",
                new { id =1 }, payload.ToUpper() ); //returns uppercase string
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdateItem([FromBody]int id, string payload)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
using DBL;
using Microsoft.AspNetCore.Mvc;
using Models;
using Org.BouncyCastle.Asn1.Tsp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace showAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        // GET: api/<ShowsController>
        [HttpGet]
        public async Task<List<Events>> GetAsync()
        {
            EventsDB eventdb = new EventsDB();
            List<Events> lst = await eventdb.selectAllAsync();
            return lst ;
        }

        // GET api/<ShowsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShowsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShowsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShowsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Excersice1.BL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excersice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        // GET: api/<InstructorsController>
        [HttpGet]
        public async Task<IEnumerable<Instructor>> Get()
        {
            return await Instructor.Read();
        }

        // GET api/<InstructorsController>/5
        [HttpGet("{id}")]
        public bool Get(int id)
        {
            return Instructor.getInstructorById(id);
        }

        // POST api/<InstructorsController>
        [HttpPost]
        public async Task<bool> Post([FromBody] Instructor instructor)
        {
            return await instructor.Insert();
        }

        // PUT api/<InstructorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InstructorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

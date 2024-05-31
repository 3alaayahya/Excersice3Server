using Microsoft.AspNetCore.Mvc;
using Excersice1.BL;

namespace Excersice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User1> Get()
        {
            return User1.Read();
        }

        [HttpGet("courses")]
        public IEnumerable<Course> GetCourses()
        {
            return User1.GetCourses();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User1 Get(int id)
        {
            return User1.Read().FirstOrDefault(u => u.Id1 == id);
        }

        // POST api/<UserController>/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User1 user)
        {
            if (user.Register())
            {
                return Ok(new { message = "Registration successful." });
            }
            else
            {
                return BadRequest(new { message = "Registration failed. Email already in use." });
            }
        }

        // POST api/<UserController>/login
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var user = User1.Login(email, password);
            if (user != null)
            {
                return Ok(new { message = "Login successful.", user });
            }
            else
            {
                return Unauthorized(new { message = "Login failed. Invalid email or password." });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout(User1 user1)
        {
            User1.Logout(user1);
            return Ok(new { message = "Logout successfully.", user1 });
        }

        // POST api/<CoursesController>
        [HttpPost("course")]
        public bool Post([FromBody] Course course)
        {
            return User1.addMyCourse(course);
        }

        [HttpGet("search")] // this uses the QueryString
        public IEnumerable<Course> GetByDurationRange(double durationFrom, double DurationTo)
        {
            User1 user = new User1();
            return user.GetByDurationRange(durationFrom, DurationTo);

        }

        [HttpGet("searchByRouting/ratingFrom/{ratingFrom}/ratingTo/{ratingTo}")]
        public IEnumerable<Course> GetByRatingRange(double ratingFrom, double ratingTo)
        {
            User1 user = new User1();
            return user.GetByRatingRange(ratingFrom, ratingTo);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("DeleteById/id/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                User1.DeleteCourse(id);
                return Ok(new { massage = "Course with id: " + id + " has been deleted successfully." }); // Return a descriptive message
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
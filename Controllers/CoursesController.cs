using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Excersice1.BL;
using static System.Net.Mime.MediaTypeNames;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excersice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            return await Course.Read();
        }

        // GET api/<CoursesController>/5
        [HttpGet("getCourseById/{id}")]
        public Course Get(int id)
        {
            return Course.getCourseById(id);
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<bool> Post([FromBody] Course course)
        {
            bool result = await course.Insert();
            if (result)
            {
                return true;
            }
            else
            {
                return false;
                //return Conflict(new { message = "Duplicate course found." });
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("update/{id}")]
        public bool Put(int id, [FromBody] Course updatedCourse)
        {
            if (updatedCourse.Update(id,updatedCourse))
            {
                updatedCourse.LastUpdate1 = DateTime.UtcNow.ToString("dd/MM/yyyy");
                return true;
            }
            return false;
            //return StatusCode(500, "Error updating course");
        }


        // POST api/<CoursesController>/uploadImage/5
        [HttpPost("uploadImage/{id}")]
        public async Task<bool> UploadImage(int id, IFormFile image)
        {
            try
            {
                var courses = await Course.Read();
                var course = courses.FirstOrDefault(c => c.Id == id);
                if (course == null)
                {
                    return false;
                }

                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);
                        var dataUrl = $"data:{image.ContentType};base64,{base64String}";

                        // Update the image reference with the Data URL of the uploaded image
                        course.ImageReference1 = dataUrl;
                        course.Update(course.Id, course);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                //Console.Error.WriteLine($"Error uploading image: {ex.Message}");
                return false;
            }
        }




        // DELETE api/<CoursesController>/5
        [HttpDelete("DeleteById/id/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Course.DeleteById(id);
                return Ok(new { massage = "Course with id: " + id + " has been deleted successfully." }); // Return a descriptive message
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

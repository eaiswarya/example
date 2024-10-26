using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace example.API.Controllers
{
    //https://localhost:8080/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET: https://localhost:8080/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Jane", "Emily", "David" };
            return Ok(studentNames);
        }
    }
}

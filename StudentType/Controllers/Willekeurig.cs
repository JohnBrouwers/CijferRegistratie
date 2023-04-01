using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace StudentType.Controllers
{
    [Route("studenttype")]
    [ApiController]
    public class Willekeurig : ControllerBase
    {

        private static readonly string[] StudentTypes = new[] { "Chipmunk", "Snatcher", "Master" };
        private static readonly Random random = new Random();

        [HttpGet]
        public ActionResult<string> Get() 
        {
            return Ok(StudentTypes[random.Next(0, StudentTypes.Length)]);
        }
    }
}

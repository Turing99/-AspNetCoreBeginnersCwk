using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : Controller
    {
        public HotelsController()
        {

        }

        [HttpGet]
        public IActionResult GetRooms()
        { return Ok("Hello"); }
    }
}

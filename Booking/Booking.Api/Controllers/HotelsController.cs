using Booking.Api.Services;
using Booking.Api.Services.Abstraction;
using Booking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly MyFirstService _myFirstService;
        private readonly ISingletonOperation _singleton;
        private readonly IScopedOperation _scoped;
        private readonly ITransientOperation _transient;
        private readonly ILogger<HotelsController> _logger;
        public HotelsController(MyFirstService firstService, ISingletonOperation singleton,
            ITransientOperation transient, IScopedOperation scoped, ILogger<HotelsController> logger)

        {
            _myFirstService = firstService;
            _singleton = singleton;
            _transient = transient;
            _scoped = scoped;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            _logger.LogInformation($"GUID of singleton: {_singleton.Guid}");
            _logger.LogInformation($"GUID of transient: {_transient.Guid}");
            _logger.LogInformation($"GUID of scoped: {_scoped.Guid}");
            var hotels = _myFirstService.GetHotels();
            return Ok(hotels);
        }


        [Route("{id}")]
        [HttpGet]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _myFirstService.GetHotels();
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _myFirstService.GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = _myFirstService.GetHotels();
            var old = hotels.FirstOrDefault(h => h.HotelId == id);

            if (old == null)
                return NotFound("No resource with the corresponding ID found");

            hotels.Remove(old);
            hotels.Add(updated);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotels = _myFirstService.GetHotels();
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);
            if (toDelete == null)
                return NotFound();

            hotels.Remove(toDelete);
            return NoContent();
        }
    }
}

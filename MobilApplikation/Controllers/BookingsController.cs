using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilApplikation.Dtos;
using MobilApplikation.Models;
using MobilApplikation.UnitOfWork;
using System.Linq;

namespace MobilApplikation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public BookingsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _uow.Bookings.GetAllAsync();
            var dtos = bookings.Select(b => b.ToDto());
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
        {
            if (string.IsNullOrWhiteSpace(bookingDto.Name) || string.IsNullOrWhiteSpace(bookingDto.Email))
            {
                return BadRequest("Name and Email are required.");
            }

            // verify performance exists
            var perfExists = _uow.Performances.Query().Any(p => p.Id == bookingDto.PerformanceId);
            if (!perfExists)
                return BadRequest("Specified performance does not exist.");

            var booking = new Booking
            {
                PerformanceId = bookingDto.PerformanceId,
                Name = bookingDto.Name,
                Email = bookingDto.Email
            };

            await _uow.Bookings.AddAsync(booking);
            await _uow.SaveAsync();
            return Ok(booking.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _uow.Bookings.GetAsync(id);
            if (booking == null)
                return NotFound();

            _uow.Bookings.Remove(booking);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}

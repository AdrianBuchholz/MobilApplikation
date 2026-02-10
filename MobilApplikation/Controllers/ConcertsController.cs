using Microsoft.AspNetCore.Mvc;
using MobilApplikation.UnitOfWork;
using MobilApplikation.Dtos;
using System.Linq;

namespace MobilApplikation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public ConcertsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetConcerts()
        {
            var concerts = await _uow.Concerts.GetAllAsync();
            var dtos = concerts.Select(c =>
            {
                // Count bookings across all performances for this concert
                var perfIds = _uow.Performances.Query().Where(p => p.ConcertId == c.Id).Select(p => p.Id);
                var bookingCount = _uow.Bookings.Query().Count(b => perfIds.Contains(b.PerformanceId));
                return c.ToDto(bookingCount);
            });
            return Ok(dtos);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilApplikation.UnitOfWork;

namespace MobilApplikation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public PerformancesController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet("{concertId}")]
        public async Task<IActionResult> GetPerformances(int concertId)
        {
            var performances = (await _uow.Performances.GetAllAsync())
               .Where(p => p.ConcertId == concertId);
            var dtos = performances.Select(p =>
            {
                var bookingCount = _uow.Bookings.Query().Count(b => b.PerformanceId == p.Id);
                return p.ToDto(bookingCount);
            });
            return Ok(dtos);
        }
    }
}

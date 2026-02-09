using MobilApplikation.Data;
using MobilApplikation.Models;
using MobilApplikation.Repositories;

namespace MobilApplikation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Concert> Concerts { get; }
        public IRepository<Performance> Performances { get; }
        public IRepository<Booking> Bookings { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Concerts = new Repository<Concert>(context);
            Performances = new Repository<Performance>(context);
            Bookings = new Repository<Booking>(context);
        }
        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();

    }
}

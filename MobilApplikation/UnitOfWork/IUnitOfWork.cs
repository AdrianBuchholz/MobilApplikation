using MobilApplikation.Models;
using MobilApplikation.Repositories;

namespace MobilApplikation.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Concert> Concerts { get; }
        IRepository<Performance> Performances { get; }
        IRepository<Booking> Bookings { get; }
        Task SaveAsync();
    }
}

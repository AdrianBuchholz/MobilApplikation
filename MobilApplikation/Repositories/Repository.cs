using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilApplikation.Data;
using Microsoft.EntityFrameworkCore;

namespace MobilApplikation.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<T?> GetAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
        {
            // AddAsync returns a ValueTask<EntityEntry<T>>; await and ignore the result
            await _context.Set<T>().AddAsync(entity);
        }

        public void Remove(T entity) =>
            _context.Set<T>().Remove(entity);

        public IQueryable<T> Query() => _context.Set<T>().AsQueryable();
    }
}

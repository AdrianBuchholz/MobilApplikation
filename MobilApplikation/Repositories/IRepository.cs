namespace MobilApplikation.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T entity);
        void Remove(T entity);
        IQueryable<T> Query();
    }
}

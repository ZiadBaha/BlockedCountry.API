using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Repositories
{
    public interface IInMemoryRepository<TKey, TEntity>
        where TEntity : class
    {
        Task<bool> AddAsync(TKey key, TEntity entity);
        Task<bool> RemoveAsync(TKey key);
        Task<bool> ExistsAsync(TKey key);
        Task<TEntity?> GetAsync(TKey key);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}

using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Services
{
    public class InMemoryRepository<TKey, TEntity> : IInMemoryRepository<TKey, TEntity>
      where TEntity : class
    {
        protected readonly ConcurrentDictionary<TKey, TEntity> _store = new();

        public Task<bool> AddAsync(TKey key, TEntity entity)
        {
            return Task.FromResult(_store.TryAdd(key, entity));
        }

        public Task<bool> RemoveAsync(TKey key)
        {
            return Task.FromResult(_store.TryRemove(key, out _));
        }

        public Task<bool> ExistsAsync(TKey key)
        {
            return Task.FromResult(_store.ContainsKey(key));
        }

        public Task<TEntity?> GetAsync(TKey key)
        {
            _store.TryGetValue(key, out var entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }
    }
}

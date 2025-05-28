using BlockedCountry.Domain.Entities;
using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Services
{
    public class TemporalBlockedCountryRepository : InMemoryRepository<string, TemporalBlockedCountry>, ITemporalBlockedCountryRepository
    {
        public Task<IEnumerable<TemporalBlockedCountry>> GetExpiredAsync(DateTime now)
        {
            var expired = _store.Values.Where(t => t.ExpirationTime <= now);
            return Task.FromResult(expired);
        }
    }
}

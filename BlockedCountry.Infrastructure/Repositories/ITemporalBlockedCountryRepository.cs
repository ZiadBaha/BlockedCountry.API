using BlockedCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Repositories
{
    public interface ITemporalBlockedCountryRepository : IInMemoryRepository<string, TemporalBlockedCountry>
    {
        Task<IEnumerable<TemporalBlockedCountry>> GetExpiredAsync(DateTime now);
    }
}

using BlockedCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Repositories
{
    public interface IBlockedAttemptLogRepository
    {
        Task AddLogAsync(BlockedAttemptLog log);
        Task<IEnumerable<BlockedAttemptLog>> GetAllLogsAsync();
    }
}

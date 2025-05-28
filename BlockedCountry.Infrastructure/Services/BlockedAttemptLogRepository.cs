using BlockedCountry.Domain.Entities;
using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Services
{
    public class BlockedAttemptLogRepository : IBlockedAttemptLogRepository
    {
        private readonly ConcurrentQueue<BlockedAttemptLog> _logs = new();

        public Task AddLogAsync(BlockedAttemptLog log)
        {
            _logs.Enqueue(log);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<BlockedAttemptLog>> GetAllLogsAsync()
        {
            return Task.FromResult(_logs.AsEnumerable());
        }
    }
}

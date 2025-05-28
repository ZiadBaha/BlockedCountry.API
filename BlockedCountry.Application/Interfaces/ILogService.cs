using BlockedCountry.Application.Common;
using BlockedCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Interfaces
{
    public interface ILogService
    {
        Task<PaginatedResponse<BlockedAttemptLog>> GetBlockedAttemptsAsync(PaginationFilter filter);
        Task AddBlockedAttemptAsync(BlockedAttemptLog logDto);
    }
}

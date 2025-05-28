using BlockedCountry.Application.Common;
using BlockedCountry.Application.Interfaces;
using BlockedCountry.Domain.Entities;
using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Services
{
    public class LogService : ILogService
    {
        private readonly IBlockedAttemptLogRepository _logRepository;

        public LogService(IBlockedAttemptLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task AddBlockedAttemptAsync(BlockedAttemptLog logDto)
        {
            var entity = new BlockedAttemptLog
            {
                IpAddress = logDto.IpAddress,
                Timestamp = logDto.Timestamp,
                CountryCode = logDto.CountryCode,
                UserAgent = logDto.UserAgent
            };
            await _logRepository.AddLogAsync(entity);
        }

        public async Task<PaginatedResponse<BlockedAttemptLog>> GetBlockedAttemptsAsync(PaginationFilter filter)
        {
            var allLogs = await _logRepository.GetAllLogsAsync();


            var totalRecords = allLogs.Count();

            var data = allLogs
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(log => new BlockedAttemptLog
                {
                    IpAddress = log.IpAddress,
                    Timestamp = log.Timestamp,
                    CountryCode = log.CountryCode,
                    UserAgent = log.UserAgent
                });

            return new PaginatedResponse<BlockedAttemptLog>
            {
                Data = data,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords
            };
        }
    }
}

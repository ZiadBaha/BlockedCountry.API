using BlockedCountry.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Services.Background
{
    public class TemporalBlockCleaner : BackgroundService
    {
        private readonly ITemporalBlockedCountryRepository _repo;
        private readonly ILogger<TemporalBlockCleaner> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

        public TemporalBlockCleaner(ITemporalBlockedCountryRepository repo, ILogger<TemporalBlockCleaner> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var expired = await _repo.GetExpiredAsync(DateTime.UtcNow);

                foreach (var item in expired)
                {
                    await _repo.RemoveAsync(item.CountryCode);
                    _logger.LogInformation("Removed expired temporal block: {CountryCode}", item.CountryCode);
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}

using BlockedCountry.Application.Common;
using BlockedCountry.Application.Interfaces;
using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Services
{
    public class BlockedCountryService : IBlockedCountryService
    {
        private readonly IBlockedCountryRepository _blockedCountryRepository;
        private readonly ConcurrentDictionary<string,Domain.Entities.BlockedCountry> _blockedCountries;

        public BlockedCountryService(IBlockedCountryRepository blockedCountryRepository)
        {
            _blockedCountryRepository = blockedCountryRepository;
            _blockedCountries = new ConcurrentDictionary<string, Domain.Entities.BlockedCountry>(StringComparer.OrdinalIgnoreCase);

        }

        public async Task<bool> AddBlockedCountryAsync(string countryCode, string countryName)
        {
            var code = countryCode.ToUpperInvariant();
            if (await _blockedCountryRepository.ExistsAsync(code))
                return false;

            var country = new Domain.Entities.BlockedCountry
            {
                CountryCode = code,
                CountryName = countryName
            };

            return await _blockedCountryRepository.AddAsync(code, country);
        }

        public async Task<bool> RemoveBlockedCountryAsync(string countryCode)
        {
            return await _blockedCountryRepository.RemoveAsync(countryCode.ToUpperInvariant());
        }

        public Task<PaginatedResponse<Domain.Entities.BlockedCountry>> GetBlockedCountriesAsync(BlockedCountryFilter filter)
        {
            var query = _blockedCountries.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.CountryCode))
                query = query.Where(c => c.CountryCode.Contains(filter.CountryCode, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filter.CountryName))
                query = query.Where(c => c.CountryName.Contains(filter.CountryName, StringComparison.OrdinalIgnoreCase));

            var totalRecords = query.Count();

            var items = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(c => new Domain.Entities.BlockedCountry
                {
                    CountryCode = c.CountryCode,
                    CountryName = c.CountryName,
                })
                .ToList();

            var response = new PaginatedResponse<Domain.Entities.BlockedCountry>
            {
                Data = items,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords
            };

            return Task.FromResult(response);
        }


        public Task<bool> IsCountryBlockedAsync(string countryCode)
        {
            return _blockedCountryRepository.ExistsAsync(countryCode.ToUpperInvariant());
        }
    }
}

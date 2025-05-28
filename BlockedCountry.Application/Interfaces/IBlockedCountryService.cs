using BlockedCountry.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Interfaces
{
    public interface IBlockedCountryService
    {
        Task<bool> AddBlockedCountryAsync(string countryCode, string countryName);
        Task<bool> RemoveBlockedCountryAsync(string countryCode);
        Task<PaginatedResponse<Domain.Entities.BlockedCountry>> GetBlockedCountriesAsync(BlockedCountryFilter filter);
        Task<bool> IsCountryBlockedAsync(string countryCode);
    }
}

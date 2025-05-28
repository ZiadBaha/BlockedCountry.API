using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Interfaces
{
    public interface IIpGeolocationService
    {
        Task<(bool Success, string? CountryCode, string? CountryName, string? Isp)> GetCountryByIpAsync(string ip);
    }
}

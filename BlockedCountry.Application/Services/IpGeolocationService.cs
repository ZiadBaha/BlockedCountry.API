using BlockedCountry.Application.Common;
using BlockedCountry.Application.Configurations;
using BlockedCountry.Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Services
{
    public class IpGeolocationService : IIpGeolocationService
    {
        private readonly HttpClient _httpClient;
        private readonly IpGeolocationSettings _settings;

        public IpGeolocationService(HttpClient httpClient, IOptions<IpGeolocationSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<(bool Success, string? CountryCode, string? CountryName, string? Isp)> GetCountryByIpAsync(string ip)
        {
            try
            {
                var url = $"{_settings.BaseUrl}?apiKey={_settings.ApiKey}&ip={ip}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return (false, null, null, null);

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<IpGeolocationResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null || string.IsNullOrEmpty(data.Country_Code2))
                    return (false, null, null, null);

                return (true, data.Country_Code2, data.Country_Name, data.Isp);
            }
            catch
            {
                return (false, null, null, null);
            }
        }
    }
}

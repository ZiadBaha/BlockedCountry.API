using BlockedCountry.Application.Common;
using BlockedCountry.Application.Interfaces;
using BlockedCountry.Application.Services;
using BlockedCountry.Domain.Entities;
using BlockedCountry.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockedCountry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedCountriesController : ControllerBase
    {
        private readonly IBlockedCountryService _blockedCountryService;
        private readonly IIpGeolocationService _ipGeolocationService;
        private readonly ITemporalBlockedCountryRepository _temporalBlockedRepo;
        private readonly IBlockedAttemptLogRepository _logRepository;
        private readonly ILogService _logService;


        public BlockedCountriesController(
            IBlockedCountryService blockedCountryService,
            IIpGeolocationService ipGeolocationService,
            ITemporalBlockedCountryRepository temporalBlockedRepo,
            IBlockedAttemptLogRepository logRepository,
            ILogService logService)
        {
            _blockedCountryService = blockedCountryService;
            _ipGeolocationService = ipGeolocationService;
            _temporalBlockedRepo = temporalBlockedRepo;
            _logRepository = logRepository;
            _logService = logService;
        }

        [HttpPost("countries/block")]
        public async Task<IActionResult> AddBlockedCountry([FromBody] Domain.Entities.BlockedCountry request)
        {
            if (string.IsNullOrWhiteSpace(request.CountryCode))
                return BadRequest("CountryCode is required.");

            var added = await _blockedCountryService.AddBlockedCountryAsync(request.CountryCode, request.CountryName);

            if (!added)
                return Conflict("Country is already blocked.");

            return Ok();
        }

        [HttpDelete("countries/block/{countryCode}")]
        public async Task<IActionResult> DeleteBlockedCountry(string countryCode)
        {
            var removed = await _blockedCountryService.RemoveBlockedCountryAsync(countryCode);
            return removed ? Ok() : NotFound();
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetBlockedCountries([FromQuery] BlockedCountryFilter filter)
        {
            var result = await _blockedCountryService.GetBlockedCountriesAsync(filter);
            return Ok(result);
        }


        [HttpGet("iplookup")]
        public async Task<IActionResult> IpLookup([FromQuery] string? ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrWhiteSpace(ipAddress))
                return BadRequest("IP address is required.");

            var (success, code, name, isp) = await _ipGeolocationService.GetCountryByIpAsync(ipAddress);
            if (!success) return NotFound("IP lookup failed");

            return Ok(new { ipAddress, countryCode = code, countryName = name, isp });
        }

        [HttpGet("ipcheckblock")]
        public async Task<IActionResult> CheckIpBlock([FromQuery] string? ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var (success, code, _, _) = await _ipGeolocationService.GetCountryByIpAsync(ipAddress);
            if (!success || string.IsNullOrWhiteSpace(code)) return NotFound("IP lookup failed");

            var isBlocked = await _blockedCountryService.IsCountryBlockedAsync(code);
            var temporal = await _temporalBlockedRepo.ExistsAsync(code);

            await _logRepository.AddLogAsync(new BlockedAttemptLog
            {
                IpAddress = ipAddress!,
                CountryCode = code,
                IsBlocked = isBlocked || temporal,
                Timestamp = DateTime.UtcNow,
                UserAgent = Request.Headers.UserAgent.ToString()
            });

            return Ok(new { ipAddress, countryCode = code, blocked = isBlocked || temporal });
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            return Ok(logs);
        }

        [HttpPost("countries/block/temporal")]
        public async Task<IActionResult> TemporalBlock([FromBody] TemporalBlockedCountry request)
        {
            if (string.IsNullOrWhiteSpace(request.CountryCode))
                return BadRequest("CountryCode is required.");

            var added = await _temporalBlockedRepo.AddAsync(request.CountryCode.ToUpper(), request);
            if (!added) return Conflict("Country already temporarily blocked");

            return Ok(request);
        }

        [HttpGet("blocked-attempts")]
        public async Task<IActionResult> GetBlockedAttempts([FromQuery] PaginationFilter filter)
        {
            var logs = await _logService.GetBlockedAttemptsAsync(filter);
            return Ok(logs);
        }
    }
}

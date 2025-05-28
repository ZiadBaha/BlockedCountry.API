using BlockedCountry.API.Controllers;
using BlockedCountry.Application.Common;
using BlockedCountry.Application.Interfaces;
using BlockedCountry.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BlockedCountry.Tests.API.Controllers
{
    public class BlockedCountriesControllerTests
    {
        private readonly Mock<IBlockedCountryService> _blockedCountryServiceMock;
        private readonly Mock<IIpGeolocationService> _ipGeolocationServiceMock;
        private readonly Mock<ITemporalBlockedCountryRepository> _temporalBlockedRepoMock;
        private readonly Mock<IBlockedAttemptLogRepository> _logRepoMock;
        private readonly Mock<ILogService> _logServiceMock;

        private readonly BlockedCountriesController _controller;

        public BlockedCountriesControllerTests()
        {
            _blockedCountryServiceMock = new Mock<IBlockedCountryService>();
            _ipGeolocationServiceMock = new Mock<IIpGeolocationService>();
            _temporalBlockedRepoMock = new Mock<ITemporalBlockedCountryRepository>();
            _logRepoMock = new Mock<IBlockedAttemptLogRepository>();
            _logServiceMock = new Mock<ILogService>();

            _controller = new BlockedCountriesController(
                _blockedCountryServiceMock.Object,
                _ipGeolocationServiceMock.Object,
                _temporalBlockedRepoMock.Object,
                _logRepoMock.Object,
                _logServiceMock.Object
            );
        }

        [Fact]
        public async Task GetBlockedCountries_Should_Return_200()
        {
            // Arrange
            var filter = new BlockedCountryFilter { Page = 1, PageSize = 10 };
            var data = new PaginatedResponse<Domain.Entities.BlockedCountry>
            {
                Data = new List<Domain.Entities.BlockedCountry> {
                    new() { CountryCode = "US", CountryName = "USA" }
                },
                Page = 1,
                PageSize = 10,
                TotalRecords = 1
            };

            _blockedCountryServiceMock
                .Setup(s => s.GetBlockedCountriesAsync(filter))
                .ReturnsAsync(data);

            // Act
            var result = await _controller.GetBlockedCountries(filter);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }
    }
}

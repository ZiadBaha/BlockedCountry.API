using BlockedCountry.Application.Services;
using BlockedCountry.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlockedCountry.Tests.Application.Services
{
    public class BlockedCountryServiceTests
    {
        private readonly Mock<IBlockedCountryRepository> _repoMock;
        private readonly BlockedCountryService _service;

        public BlockedCountryServiceTests()
        {
            _repoMock = new Mock<IBlockedCountryRepository>();
            _service = new BlockedCountryService(_repoMock.Object);
        }

        [Fact]
        public async Task AddBlockedCountryAsync_Should_Add_New_Country()
        {
            // Arrange
            var code = "US";
            var name = "United States";
            _repoMock.Setup(r => r.ExistsAsync(code)).ReturnsAsync(false);
            _repoMock.Setup(r => r.AddAsync(code, It.IsAny<Domain.Entities.BlockedCountry>())).ReturnsAsync(true);

            // Act
            var result = await _service.AddBlockedCountryAsync(code, name);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddBlockedCountryAsync_Should_Reject_Duplicates()
        {
            var code = "US";
            _repoMock.Setup(r => r.ExistsAsync(code)).ReturnsAsync(true);

            var result = await _service.AddBlockedCountryAsync(code, "United States");

            result.Should().BeFalse();
        }
    }
}

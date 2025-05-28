using BlockedCountry.Infrastructure.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlockedCountry.Tests.Infrastructure.Repositories
{
    public class InMemoryRepositoryTests
    {
        private readonly InMemoryRepository<string, Domain.Entities.BlockedCountry> _repo;

        public InMemoryRepositoryTests()
        {
            _repo = new();
        }

        [Fact]
        public async Task AddAsync_Should_Add_Entity()
        {
            var entity = new Domain.Entities.BlockedCountry { CountryCode = "US", CountryName = "United States" };
            var result = await _repo.AddAsync("US", entity);

            result.Should().BeTrue();
            var stored = await _repo.GetAsync("US");
            stored.Should().NotBeNull();
            stored!.CountryCode.Should().Be("US");
        }
    }

}

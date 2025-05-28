using BlockedCountry.Application.DTOs;
using BlockedCountry.Application.Validators;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlockedCountry.Tests.Application.Validators
{
    public class TemporalBlockRequestValidatorTests
    {
        private readonly TemporalBlockRequestValidator _validator = new();

        [Theory]
        [InlineData("US", 60)]
        [InlineData("EG", 1440)]
        public void Should_Validate_Valid_Requests(string countryCode, int duration)
        {
            var model = new TemporalBlockRequest { CountryCode = countryCode, DurationMinutes = duration };
            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("XX", 60)]
        [InlineData("US", 0)]
        public void Should_Fail_Invalid_Requests(string countryCode, int duration)
        {
            var model = new TemporalBlockRequest { CountryCode = countryCode, DurationMinutes = duration };
            var result = _validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
    }

}

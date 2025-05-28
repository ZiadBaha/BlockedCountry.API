using BlockedCountry.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Validators
{
    public class TemporalBlockedCountryValidator : AbstractValidator<TemporalBlockedCountry>
    {
        public TemporalBlockedCountryValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.");

            RuleFor(x => x.ExpirationTime)
                .Must(BeInTheFuture)
                .WithMessage("Expiration time must be in the future.");
        }

        private bool BeInTheFuture(DateTime expirationTime)
        {
            return expirationTime > DateTime.UtcNow;
        }
    }
}

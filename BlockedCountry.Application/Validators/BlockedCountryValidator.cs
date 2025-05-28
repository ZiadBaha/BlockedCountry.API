using FluentValidation;
using BlockedCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Validators
{
    public class BlockedCountryValidator : AbstractValidator<Domain.Entities.BlockedCountry>
    {
        public BlockedCountryValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.");

            RuleFor(x => x.CountryName)
                .NotEmpty()
                .WithMessage("Country name is required.");
        }
    }
}

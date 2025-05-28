using BlockedCountry.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Validators
{
    public class TemporalBlockRequestValidator : AbstractValidator<TemporalBlockRequest>
    {
        public TemporalBlockRequestValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required.")
                .Length(2).WithMessage("Country code must be exactly 2 characters.")
                .Matches("^[A-Z]{2}$").WithMessage("Country code must be a valid ISO Alpha-2 code (uppercase only).");

            RuleFor(x => x.DurationMinutes)
                .NotNull().WithMessage("Duration is required.")
                .InclusiveBetween(1, 1440)
                .WithMessage("Duration must be between 1 and 1440 minutes.");
        }
    }
}

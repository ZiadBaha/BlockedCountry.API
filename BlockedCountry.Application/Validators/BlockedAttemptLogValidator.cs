using BlockedCountry.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Validators
{
    public class BlockedAttemptLogValidator : AbstractValidator<BlockedAttemptLog>
    {
        public BlockedAttemptLogValidator()
        {
            RuleFor(x => x.IpAddress)
                .NotEmpty()
                .WithMessage("IP address is required.")
                .Matches(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")
                .WithMessage("Invalid IP address format.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.");

            RuleFor(x => x.Timestamp)
                .NotEmpty()
                .WithMessage("Timestamp is required.");

            RuleFor(x => x.UserAgent)
                .NotEmpty()
                .WithMessage("User agent is required.");
        }
    }
}

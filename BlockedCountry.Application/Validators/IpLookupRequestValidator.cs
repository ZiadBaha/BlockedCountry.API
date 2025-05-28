using BlockedCountry.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Validators
{
    public class IpLookupRequestValidator : AbstractValidator<IpLookupRequest>
    {
        public IpLookupRequestValidator()
        {
            RuleFor(x => x.IpAddress)
                .Cascade(CascadeMode.Stop)
                .Must(BeValidIpOrNull)
                .WithMessage("Invalid IP address format.");
        }

        private bool BeValidIpOrNull(string? ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return true; 

            var ipv4Regex = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$";
            return Regex.IsMatch(ip, ipv4Regex);
        }
    }
}

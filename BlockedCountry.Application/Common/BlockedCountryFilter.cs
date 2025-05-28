using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Common
{
    public class BlockedCountryFilter : PaginationFilter
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
    }
}

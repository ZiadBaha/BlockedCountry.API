using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Domain.Entities
{
    public class TemporalBlockedCountry
    {
        public string CountryCode { get; set; } = null!;
        public DateTime ExpirationTime { get; set; }
    }

}

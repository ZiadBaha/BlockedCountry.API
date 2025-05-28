using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Domain.Entities
{
    public class BlockedAttemptLog
    {
        public string IpAddress { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public bool IsBlocked { get; set; }
        public string UserAgent { get; set; } = null!;
    }
}

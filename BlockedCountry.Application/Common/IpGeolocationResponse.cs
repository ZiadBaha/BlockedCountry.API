using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Application.Common
{
    public class IpGeolocationResponse
    {
        public string Country_Code2 { get; set; } = null!;
        public string Country_Name { get; set; } = null!;
        public string Isp { get; set; } = null!;
    }
}

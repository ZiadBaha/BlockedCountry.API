using BlockedCountry.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Services
{
    public class BlockedCountryRepository : InMemoryRepository<string, Domain.Entities.BlockedCountry>, IBlockedCountryRepository 
    {

    }

}

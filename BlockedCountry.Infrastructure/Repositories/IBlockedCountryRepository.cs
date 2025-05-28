using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockedCountry.Infrastructure.Repositories
{
    public interface IBlockedCountryRepository : IInMemoryRepository<string, Domain.Entities.BlockedCountry> 
    {

    }

}

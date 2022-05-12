using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfrastructure.Repositories
{
    internal class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

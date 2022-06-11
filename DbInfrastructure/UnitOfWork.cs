using DbInfrastructure.Repositories;
using Domain;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IceWallOwDbContext _dbContext;
        public ICartRepository CartRepository => new CartRepository(_dbContext);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_dbContext);

        public IProductRepository ProductRepository => new ProductRepository(_dbContext);

        public IUserRepository UserRepository => new UserRepository(_dbContext);

        public IAddressRepository AddressRepository => new AddressRepository(_dbContext);

        public UnitOfWork()
        {
            _dbContext = new IceWallOwDbContext();
            _dbContext.Database.Migrate();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

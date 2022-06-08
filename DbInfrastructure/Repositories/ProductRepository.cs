using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        public ProductRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {          
        }
        new public async Task<List<Product>> FindAllAsync()
        {
            return await _dbContext.Product
                .Include(x => x.Category)
                .ToListAsync();
          
        }

        public async Task<List<Product>> FindByName(string name)
        {
            return await _dbContext.Product.Where(x => x.Name == name)
                .Include(x => x.Category)
                .ToListAsync();
        }

        //public async Task<List<Product>> FindAllAsync()
        //{
        //    var list = await _dbContext.Product.ToListAsync();
        //    foreach (var product in list)
        //    {
        //        product.Category = await _dbContext.Category.Where(x => x.Products.Contains(product)).FirstOrDefaultAsync();
        //    }
        //    return list;
        //}
    }
}

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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {
            //_dbContext = dbContext;
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _dbContext.Category.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }
    }
}

using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<List<Product>> FindByName(string name);
    }
}

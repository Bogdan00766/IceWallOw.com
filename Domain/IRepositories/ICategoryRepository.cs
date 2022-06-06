using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<List<Category>> FindByName(string name);
    }
}

using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByEmail(string email);
        bool CheckPassword(string email, byte[] hash);
    }
}

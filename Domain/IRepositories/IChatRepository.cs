using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IChatRepository : IRepository<Chat>
    {
        Chat? FindByUsers(User user1, User user2);
    }
}

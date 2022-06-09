using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfrastructure.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {

        }

        public Chat? FindByUsers(User user1, User user2)
        {
            return _dbContext.Chat.Where(x => x.Users.Contains(user1) && x.Users.Contains(user2)).FirstOrDefault();
        }
    }
}

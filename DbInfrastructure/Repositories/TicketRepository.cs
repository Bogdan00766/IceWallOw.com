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
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {

        }
        public new async Task<Ticket> FindByIdAsync(int id)
        {
            return await _dbContext.Ticket.Where(x => x.Id == id).Include(x => x.Chat).Include(x => x.Chat.Users).FirstOrDefaultAsync();
        }
    }
}

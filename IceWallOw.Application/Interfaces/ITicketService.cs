using IceWallOw.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Interfaces
{
    public interface ITicketService
    {
        UserDto? FindUserByGuid(Guid guid);
        TicketDto NewTicket(TicketDto ticket, UserDto user);
        TicketDto GetTicketById(int id);
    }
}

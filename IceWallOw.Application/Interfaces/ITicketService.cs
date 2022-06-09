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
        Task<TicketDto> GetTicketById(int id);
        ChatDto GetChatById(int id);
        ChatDto GetChatByUsers(UserDto user1, UserDto user2);
        ChatDto NewChat(UserDto user1, UserDto user2);
    }
}

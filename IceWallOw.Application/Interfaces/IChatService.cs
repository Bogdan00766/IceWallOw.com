using IceWallOw.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Interfaces
{
    public interface IChatService
    {
        UserDto? FindUserByGuid(Guid guid);
        Task PutMessage(MessageDto message);
        Task<ICollection<MessageDto>> GetMessages(ChatDto chat);
    }
}

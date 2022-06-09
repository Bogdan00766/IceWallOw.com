using IceWallOw.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Interfaces
{
    public interface IMessageService
    {
        MessageDto Send(UserDto from, UserDto to, string content);
    }
}

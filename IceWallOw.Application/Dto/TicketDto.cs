using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Dto
{
    public class TicketDto
    {
        public int Id { get; }
        public ChatDto Chat { get; set; }
        public TicketDto(int id)
        {
            Id = id;
        }
    }
}

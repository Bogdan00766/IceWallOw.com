using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Dto
{
    public class TicketDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public ChatDto Chat { get; set; }

    }
}

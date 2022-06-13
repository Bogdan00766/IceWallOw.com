using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Dto
{
    public class MessageDto
    {
        public string Content { get; set; }
        public DateTime? Date { get; set; }
        public int ChatId { get; set; }
        public UserDto? Owner { get; set; }
        public Guid? OwnerGuid { get; set; }
    }
}

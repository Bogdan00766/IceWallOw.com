using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Classes
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<string>? Messages { get; set; }
    }
}

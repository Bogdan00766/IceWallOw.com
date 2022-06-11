using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ticket : Entity
    {
        public string Title { get; set; }
        public Chat Chat { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}

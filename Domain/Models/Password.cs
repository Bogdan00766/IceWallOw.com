using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Password : Entity
    {
        public String Key { get; set; }
        public String Salt { get; set; }
        public User User { get; set; }
    }
}

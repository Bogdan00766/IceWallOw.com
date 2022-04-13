using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Cart : Entity
    {
        public User Owner { get; set; }
        public Product Product { get; set; }
    }
}

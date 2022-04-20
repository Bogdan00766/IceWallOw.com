using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cart : Entity
    {
        public string Name { get; set; }
        public User Owner { get; set; }
        public Product Product { get; set; }
    }
}

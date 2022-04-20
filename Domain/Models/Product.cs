using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : Entity
    {
        [MaxLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public Category Category { get; set; }
    }
}

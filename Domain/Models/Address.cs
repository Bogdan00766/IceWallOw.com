using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Address : Entity
    {
        public string Country { get; set; }
        [MaxLength(32)]
        public string City { get; set; }
        [MaxLength(32)]
        public string Street { get; set; }
        [MaxLength(12)]
        public string HomeNumber { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        public User User { get; set; }
    }
}

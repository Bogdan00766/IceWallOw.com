using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message : Entity
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public User SentFrom { get; set; }
        public Chat Chat { get; set; }
    }
}

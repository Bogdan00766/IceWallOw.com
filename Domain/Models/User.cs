using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Models
{
    public class User : Entity
    {        
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(32)]
        public string EMail { get; set; }
        public byte[] Password { get; set; }
        public string AutoLoginGUID { get; set; }
        public DateTime AutoLoginGUIDExpires { get; set; }
        public List<Cart> Cart { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }
    }
}

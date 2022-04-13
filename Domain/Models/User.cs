using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class User : Entity
    {
        //TODO dodać kolumny dla tabeli
        public String Name { get; set; }
        public String LastName { get; set; }
        public String EMail { get; set; }       
        public Password Password { get; set; }
    }
}

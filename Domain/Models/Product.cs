using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Product : Entity
    {
        //TODO dodać kolumny dla tabeli
        public Category category { get; set; }
    }
}

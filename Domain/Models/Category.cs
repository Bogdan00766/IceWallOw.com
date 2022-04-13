using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Category : Entity
    {
        //TODO dodać kolumny dla tabeli
        public List<Product> Products { get; set; }
    }
}

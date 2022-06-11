﻿using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chat : Entity
    {
        public List <User> Users{ get; set; }       
        public List<Message> Messages { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        //public UserDto SentFrom { get; set; }
        public int ChatId { get; set; }
    }
}

﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Dto
{
    public class ChatDto
    {
        public int Id { get; set; }
        public List<UserDto> Users { get; set; }
    }
}

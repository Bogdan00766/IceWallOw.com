﻿using AutoMapper;
using Domain.Models;
using IceWallOw.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
            cfg.CreateMap<Product, ProductDto>();
            cfg.CreateMap<CreateProductDto, Product>();
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<Ticket, TicketDto>();
            cfg.CreateMap<TicketDto, Ticket>();
            cfg.CreateMap<Chat, ChatDto>();
            cfg.CreateMap<ChatDto, Chat>();
            cfg.CreateMap<List<Message>, List<MessageDto>> ();
            })
            .CreateMapper();
        
    }
}

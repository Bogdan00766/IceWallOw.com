using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(IUserRepository userRepository, IChatRepository chatRepository, ITicketRepository ticketRepository IMapper mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public TicketDto NewTicket(TicketDto ticket, UserDto user)
        {
            Chat cht = new Chat();
            cht.Users.Add(_userRepository.FindByEmail(user.EMail));
            cht = _chatRepository.Create(cht);

            Ticket tic = new Ticket()
            {
                Title = ticket.Title,
                CreationTime = DateTime.Now,
                Chat = cht,
            };

            tic = _ticketRepository.Create(tic);
            _ticketRepository.SaveAsync();
            return _mapper.Map<TicketDto>(tic);
        }

        public async Task<TicketDto> GetTicketById(int id)
        {
            return _mapper.Map<TicketDto>(await _ticketRepository.FindByIdAsync(id));
        }

        public UserDto FindUserByGuid(Guid guid)
        {
            return _mapper.Map<UserDto>(_userRepository.FindUserByGUID(guid));
            
        }

    }
}

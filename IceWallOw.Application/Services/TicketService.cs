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

        public TicketService(IUserRepository userRepository, IChatRepository chatRepository, ITicketRepository ticketRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public TicketDto NewTicket(TicketDto ticket, UserDto user)
        {
            Chat cht = new Chat()
            {
                Users = new List<User>() { _userRepository.FindByEmail(user.EMail)  }
            };

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
            var ticket = _mapper.Map<TicketDto>(await _ticketRepository.FindByIdAsync(id));
            return ticket;
        }
        public async Task<TicketDto> ClaimTicketById(int id, UserDto user)
        {
            var ticket = await _ticketRepository.FindByIdAsync(id);
            var userDb = await _userRepository.FindByIdAsync(user.Id);
            ticket.Chat.Users.Add(userDb);
            _ticketRepository.Update(ticket);
            return await GetTicketById(id);
        }

        public UserDto FindUserByGuid(Guid guid)
        {
            return _mapper.Map<UserDto>(_userRepository.FindUserByGUID(guid));
            
        }

        public ChatDto GetChatById(int id)
        {
            return _mapper.Map<ChatDto>(_chatRepository.FindByIdAsync(id));
        }

        public ChatDto GetChatByUsers(UserDto user1, UserDto user2)
        {
            var usr1 = _userRepository.FindByEmail(user1.EMail);
            var usr2 = _userRepository.FindByEmail(user2.EMail);
            return _mapper.Map<ChatDto>(_chatRepository.FindByUsers(usr1, usr2));
        }

        public ChatDto NewChat(UserDto user1, UserDto user2)
        {
            Chat chat = new Chat();
            chat.Users.Add(_userRepository.FindByEmail(user1.EMail));
            chat.Users.Add(_userRepository.FindByEmail(user2.EMail));
            chat = _chatRepository.Create(chat);
            _chatRepository.SaveAsync();

            return _mapper.Map<ChatDto>(chat);
        }
    }
}

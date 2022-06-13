using AutoMapper;
using Domain.IRepositories;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Services
{
    public class WebSocketService : IChatService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public WebSocketService(IUserRepository userRepository, IChatRepository chatRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public UserDto? FindUserByGuid(Guid guid)
        {
            return _mapper.Map<UserDto?>(_userRepository.FindUserByGUID(guid));
        }

        public Task<ICollection<MessageDto>> GetMessages(ChatDto chat)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDto> PutMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}

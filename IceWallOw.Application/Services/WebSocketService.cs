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
    public class WebSocketService : IWebSocketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public WebSocketService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDto? FindUserByGuid(Guid guid)
        {
            return _mapper.Map<UserDto?>(_userRepository.FindUserByGUID(guid));
        }
    }
}

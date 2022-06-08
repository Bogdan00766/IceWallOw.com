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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> ChangeEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}

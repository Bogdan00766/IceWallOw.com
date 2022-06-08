using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public bool ChangeEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(string email, string password)
        {
            if (email == null) throw new Exception("Email cannot be null");
            if (password == null) throw new Exception("Password cannot be null");
            var user = _userRepository.FindByEmail(email);
            if (user == null) throw new Exception("User not found");
            
            byte[] hash;
            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                hash = sha256.ComputeHash(hash);
            }
            if (_userRepository.CheckPassword(email, hash)) return _mapper.Map<UserDto>(user);
            else throw new Exception("Wrong password");
        }

        public async Task<bool> Logout(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user.IsLogged == false) throw new Exception("User is not logged");
            user.IsLogged = false;
            _userRepository.SaveAsync();
            return true;
        }

        public UserDto Register(string name, string lastName, string password, string email)
        {
            if(name == null) throw new Exception("Name cannot be null");
            if(lastName == null) throw new Exception("Last Name cannot be null");
            if(password == null) throw new Exception("Password, cannot be null");
            if(email == null) throw new Exception("Email cannot be null");

            password = name + email;

            byte[] hash;
            using (SHA256 sha256 = SHA256.Create()) 
            {
                hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                hash = sha256.ComputeHash(hash);
            }
            User user = new User()
            {
                Name = name,
                LastName = lastName,
                Password = hash,
                EMail = email,
                IsLogged = false,
            };
            
            var usr = _userRepository.Create(user);
            _userRepository.SaveAsync();

            return _mapper.Map<UserDto>(usr);
        }
       
    }
}

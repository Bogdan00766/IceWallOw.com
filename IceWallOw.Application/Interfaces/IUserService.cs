using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Interfaces
{
    internal interface IUserService
    {
        Task<UserDto> Login(string username, string password);
        Task<bool> Logout(string username, string password);
        Task<UserDto> Register(User user);
        Task<bool> ChangePassword(string password);
        Task<bool> ForgotPassword(string email);
        Task<bool> ChangeEmail(string email);
    }
}

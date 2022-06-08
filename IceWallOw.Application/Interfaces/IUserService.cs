﻿using Domain.Models;
using IceWallOw.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Interfaces
{
    public interface IUserService
    {
        UserDto Login(string username, string password);
        Task<bool> Logout(int id);
        UserDto Register(string name, string lastName, string password, string email);
        bool ChangePassword(int id, string newpassword);
        bool ForgotPassword(string email);
        //bool ChangeEmail(string email);
    }
}
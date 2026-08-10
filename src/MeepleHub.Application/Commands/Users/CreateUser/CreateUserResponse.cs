using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Users.CreateUser
{
    public class CreateUserResponse
    {
        public UserDto User { get; set; } = null!;
    }
}

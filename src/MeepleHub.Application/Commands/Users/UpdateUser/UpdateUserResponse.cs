using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Users.UpdateUser
{
    public class UpdateUserResponse
    {
        public bool Updated { get; set; }
        public bool EmailAlreadyExists { get; set; }
        public UserDto? User { get; set; }
    }
}

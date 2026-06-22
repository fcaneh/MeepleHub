using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Games.Users.GetUserById
{
    public class GetUserByIdResponse
    {
        public UserDto? User { get; set; }
    }
}

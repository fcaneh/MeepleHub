using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Users.GetUsers
{
    public class GetUsersResponse
    {
        public IEnumerable<UserDto> Users{ get; set; } = Enumerable.Empty<UserDto>();
    }
}

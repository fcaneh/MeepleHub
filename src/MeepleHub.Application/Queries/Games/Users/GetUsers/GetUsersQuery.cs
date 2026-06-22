using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.Users.GetUsers
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
    }
}

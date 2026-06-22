using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public int Id { get; set; }
    }
}

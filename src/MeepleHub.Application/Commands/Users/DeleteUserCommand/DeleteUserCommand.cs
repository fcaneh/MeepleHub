using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Users.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public int Id { get; set; }
    }
}

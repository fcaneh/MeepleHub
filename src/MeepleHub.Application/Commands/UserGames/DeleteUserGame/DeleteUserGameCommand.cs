using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.UserGames.DeleteUserGame
{
    public class DeleteUserGameCommand : IRequest<DeleteUserGameResponse>
    {
        public int Id { get; set; }
    }
}

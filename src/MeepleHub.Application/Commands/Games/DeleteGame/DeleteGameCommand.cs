using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Games.DeleteGame
{
    public class DeleteGameCommand : IRequest<DeleteGameResponse>
    {
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.UserGames.GetUserGameById
{
    public class GetUserGameByIdQuery : IRequest<GetUserGameByIdResponse>
    {
        public int UserGameId { get; set; }
    }
}

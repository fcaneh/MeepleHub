using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.UserGames.GetUserGames
{
    public class GetUserGamesQuery : IRequest<GetUserGamesResponse>
    {
        public int UserId { get; set; }
    }
}

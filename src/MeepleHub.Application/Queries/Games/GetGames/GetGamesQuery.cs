using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.GetGames
{
    public class GetGamesQuery : IRequest<GetGamesResponse>
    {
        public string? NameFilter { get; set; }
    }
}

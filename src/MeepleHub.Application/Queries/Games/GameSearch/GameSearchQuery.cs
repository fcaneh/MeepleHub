using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.GameSearch
{
    public class GameSearchQuery : IRequest<GameSearchResponse>
    {
        public string Query { get; set; } = string.Empty;
    }
}

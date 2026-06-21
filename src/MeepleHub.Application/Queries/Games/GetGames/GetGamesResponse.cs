using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Games.GetGames
{
    public class GetGamesResponse
    {
        public IEnumerable<GameDto> Games { get; set; } = Enumerable.Empty<GameDto>();
    }
}

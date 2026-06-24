using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Games.GameSearch
{
    public class GameSearchResponse
    {
        public IEnumerable<GameDto> Games { get; set; } = Enumerable.Empty<GameDto>();
    }
}

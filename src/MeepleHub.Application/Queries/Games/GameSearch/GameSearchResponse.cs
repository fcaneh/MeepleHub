using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Games.GameSearch
{
    public class GameSearchResponse
    {
        public IEnumerable<GameDto> LocalGames { get; set; } = Enumerable.Empty<GameDto>();
        public IEnumerable<BggSearchResultDto> ExternalGames { get; set; } = Enumerable.Empty<BggSearchResultDto>();
        public bool HasLocalResults {get; set; }
        public string? ErrorMessage { get; set; }
    }
}

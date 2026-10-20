using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.GetGames
{
    public class GetGamesQuery : IRequest<GetGamesResponse>
    {
        public string? NameFilter { get; set; }
        public List<int>? CategoryIds { get; set; }
        public int? NumberPlayersFilter { get; set; }
        public int? MinPlayersFilter { get; set; }
        public int? MaxPlayersFilter { get; set; }
        public List<int>? MechanicIds { get; set; }
        public List<int>? PublisherIds { get; set; }
        public List<int>? DesignerIds { get; set; }
        public int? YoungestPlayerAge { get; set; }
    }
}

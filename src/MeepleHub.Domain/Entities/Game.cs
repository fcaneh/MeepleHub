using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int? PublishedYear { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int MinPlayTime { get; set; }

        public int MaxPlayTime { get; set; }

        public int MinAge { get; set; }

        public decimal Complexity { get; set; }

        public decimal RetailPrice { get; set; }

        public int PublisherId { get; set; }

        public Publisher? Publisher { get; set; }
        public List<UserGame>? UserGames { get; set; }
        public List<GameAlias>? Aliases { get; set; }
        public List<GameExternalReference>? ExternalReferences { get; set; }
    }
}

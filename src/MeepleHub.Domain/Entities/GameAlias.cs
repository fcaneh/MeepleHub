using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class GameAlias
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LanguageCode { get; set; }
    }
}

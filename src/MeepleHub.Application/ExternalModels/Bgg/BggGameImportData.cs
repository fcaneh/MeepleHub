using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.ExternalModels.Bgg
{
    public class BggGameImportData
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? PublishedYear { get; set; }
        public string? PublisherName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MinPlayTime { get; set; }
        public int MaxPlayTime { get; set; }
        public int MinAge { get; set; }
        public decimal Complexity { get; set; }
        public List<string> Aliases { get; set; } = new();
    }
}

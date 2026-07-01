namespace MeepleHub.Api.Requests.Games
{
    public class UpdateGameRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        public string? ImageUrl { get; set; }
        public int PublisherId { get; set; }
        public string? Description { get; set; }
        public int? PublishedYear { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MinPlayTime { get; set; }
        public int MaxPlayTime { get; set; }
        public int MinAge { get; set; }
        public decimal Complexity { get; set; }
    }
}

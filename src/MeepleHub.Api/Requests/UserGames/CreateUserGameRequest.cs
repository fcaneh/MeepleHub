using MeepleHub.Domain.Enums;

namespace MeepleHub.Api.Requests.UserGames
{
    public class CreateUserGameRequest
    {
        public int GameId { get; set; }
        public Status Status { get; set; }
        public Condition Condition { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public int? PersonalRating { get; set; }
        public string? Notes { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Domain.Entities
{
    public class UserGame
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public Status Status { get; set; }
        public Condition Condition { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public int? PersonalRating { get; set; }
        public string? Notes { get; set; }
        public User? User { get; set; }
        public Game? Game { get; set; }
    }
}

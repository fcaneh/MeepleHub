using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Application.DTOs
{
    public class UserGameDto
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Application.Commands.UserGames.UpdateUserGame
{
    public class UpdateUserGameCommand :IRequest<UpdateUserGameResponse>
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Condition Condition { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public int? PersonalRating { get; set; }
        public string? Notes { get; set; } = string.Empty;
    }
}

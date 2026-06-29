using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Domain.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public int RequesterUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public TradeStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? DeclinedAt { get; set; }
        public string? Notes { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; } = new List<TradeItem>();
    }
}

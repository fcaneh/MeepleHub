using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class TradeItem
    {
        public int Id { get; set; }

        public int TradeId { get; set; }
        public Trade? Trade { get; set; }

        public int UserGameId { get; set; }
        public UserGame? UserGame { get; set; }

        public int RequestedByUserId { get; set; }
        public User? RequestedByUser { get; set; }
    }
}

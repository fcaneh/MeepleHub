using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class TradeItem
    {
        public int Id { get; set; }
        public int TradeId { get; set; }
        public int UserGameId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public Trade? Trade { get; set; }
        public UserGame? UserGame { get; set; }
        public User? FromUser { get; set; }
        public User? ToUser { get; set; }
    }
}

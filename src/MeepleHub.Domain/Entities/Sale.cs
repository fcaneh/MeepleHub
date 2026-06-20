using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}

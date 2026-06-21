using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class Exchange
    {
        public int Id { get; set; }
        public int ExchangerOneId { get; set; }
        public int ExchangerOneGameId { get; set; }
        public int ExchangerTwoId { get; set; }
        public int ExchangerTwoGameId { get; set; }
    }
}

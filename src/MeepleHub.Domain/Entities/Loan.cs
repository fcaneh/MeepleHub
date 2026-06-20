using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class Loan
    {
       public int Id { get; set; }
       public int GameId { get; set; }
       public int LenderId { get; set; }
       public int BorrowerId { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
    }
}

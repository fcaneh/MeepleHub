using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserGameId { get; set; }
        public UserGame? UserGame { get; set; }
        public int BorrowerUserId { get; set; }
        public User? BorrowerUser { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string? Notes { get; set; }
        public LoanStatus Status { get; set; }
    }
}

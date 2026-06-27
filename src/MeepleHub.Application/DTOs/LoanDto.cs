using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int UserGameId { get; set; }
        public int BorrowerUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string? Notes { get; set; }
        public DateTime RequestDate { get; set; }
        public LoanStatus Status { get; set; }
    }
}

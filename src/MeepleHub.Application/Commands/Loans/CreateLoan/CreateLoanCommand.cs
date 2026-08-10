using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Application.Commands.Loans.CreateLoan
{
    public class CreateLoanCommand :IRequest<CreateLoanResponse>
    {
        public int UserId { get; set; }
        public int UserGameId { get; set; }
        public int BorrowerUserId { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string? Notes { get; set; }
    }
}

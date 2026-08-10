using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Loans.AcceptLoan
{
    public class AcceptLoanCommand : IRequest<AcceptLoanResponse>
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
    }
}

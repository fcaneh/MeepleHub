using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Loans.CancelLoan
{
    public class CancelLoanCommand : IRequest<CancelLoanResponse>
    {
        public int BorrowerId { get; set; }
        public int LoanId { get; set; }
    }
}

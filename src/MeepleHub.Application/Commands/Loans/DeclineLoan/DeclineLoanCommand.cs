using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Loans.DeclineLoan
{
    public class DeclineLoanCommand : IRequest<DeclineLoanResponse>
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
    }
}

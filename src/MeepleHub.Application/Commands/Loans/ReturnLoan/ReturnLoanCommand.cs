using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Application.Commands.Loans.ReturnLoan
{
    public class ReturnLoanCommand :IRequest<ReturnLoanResponse>
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public string? Notes { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Loans.AcceptLoan
{
    public class AcceptLoanResponse
    {
        public bool Accepted { get; set; }
        public LoanDto? Loan { get; set; }
    }
}

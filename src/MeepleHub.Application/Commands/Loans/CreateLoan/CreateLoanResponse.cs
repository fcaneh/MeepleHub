using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Loans.CreateLoan
{
    public class CreateLoanResponse
    {
        public LoanDto Loan { get; set; } = null!;
    }
}

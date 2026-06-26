using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Loans.GetUserGameLoans
{
    public class GetUserGameLoansResponse
    {
        public IEnumerable<LoanDto> Loans { get; set; } = Enumerable.Empty<LoanDto>();
    }
}

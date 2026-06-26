using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Application.Queries.Loans.GetUserLoans
{
    public class GetUserLoansResponse
    {
        public IEnumerable<LoanDto> Loans { get; set; } = Enumerable.Empty<LoanDto>();
    }
}

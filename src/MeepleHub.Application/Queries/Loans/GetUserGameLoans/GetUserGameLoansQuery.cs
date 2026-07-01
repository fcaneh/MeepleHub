using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Loans.GetUserGameLoans
{
    public class GetUserGameLoansQuery : IRequest<GetUserGameLoansResponse>
    {
        public int UserGameId { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Loans.GetUserLoans
{
    public class GetUserLoansQuery :IRequest<GetUserLoansResponse>
    {
        public int UserId { get; set; }
    }
}

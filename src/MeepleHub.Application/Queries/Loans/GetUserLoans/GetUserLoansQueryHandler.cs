using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Loans.GetUserLoans
{
    public class GetUserLoansQueryHandler : IRequestHandler<GetUserLoansQuery, GetUserLoansResponse>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetUserLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        } 

        public async Task<GetUserLoansResponse> Handle(GetUserLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetLoansForUserAsync(request.UserId);
            return new GetUserLoansResponse
            {
                Loans = _mapper.Map<IEnumerable<LoanDto>>(loans)
            };
        }
    }
}

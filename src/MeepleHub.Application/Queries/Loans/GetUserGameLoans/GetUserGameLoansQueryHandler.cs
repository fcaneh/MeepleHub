using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Loans.GetUserGameLoans
{
    public class GetUserGameLoansQueryHandler : IRequestHandler<GetUserGameLoansQuery, GetUserGameLoansResponse>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetUserGameLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<GetUserGameLoansResponse> Handle(GetUserGameLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetLoansForUserGameAsync(request.UserId, request.UserGameId);
            return new GetUserGameLoansResponse
            {
                Loans = _mapper.Map<IEnumerable<LoanDto>>(loans)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Loans.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, CreateLoanResponse>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<CreateLoanResponse> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var hasActiveLoan = await _loanRepository.HasActiveLoanForUserGameAsync(request.UserGameId);

            if (hasActiveLoan)
            {
                throw new InvalidOperationException("This game is already loaned");
            }

            var loan = new Loan
            {
                UserGameId = request.UserGameId,
                BorrowerUserId = request.BorrowerUserId,
                StartDate = request.StartDate,
                ExpectedReturnDate = request.ExpectedReturnDate,
                Notes = request.Notes,
            };

            await _loanRepository.AddAsync(loan);
            await _loanRepository.SaveChangesAsync();
            return new CreateLoanResponse
            {
                Loan = _mapper.Map<LoanDto>(loan)
            };
        }
    }
}

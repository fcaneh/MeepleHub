using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Enums;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Loans.AcceptLoan
{
    public class AcceptLoanCommandHandler : IRequestHandler<AcceptLoanCommand, AcceptLoanResponse>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public AcceptLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<AcceptLoanResponse> Handle(AcceptLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanInvolvingUserAsync(request.UserId, request.LoanId);
            
            if (loan is null)
            {
                return new AcceptLoanResponse { Accepted = false };
            }

            if (loan.UserGame?.UserId != request.UserId )
            {
                return new AcceptLoanResponse { Accepted = false };
            }

            if (loan.Status != LoanStatus.Pending)
            {
                return new AcceptLoanResponse { Accepted = false };
            }


            var isUserGameAlreadyBorrowed = await _loanRepository.HasActiveLoanForUserGameAsync(loan.UserGameId);
            if (isUserGameAlreadyBorrowed) 
            {
                throw new InvalidOperationException("This game is already loaned");
            }

            loan.Status = LoanStatus.Accepted;
            loan.StartDate = request.StartDate;
            
            if(request.ExpectedReturnDate is not null)
            {
                loan.ExpectedReturnDate = request.ExpectedReturnDate;
            }
            await _loanRepository.SaveChangesAsync();
            
            return new AcceptLoanResponse
            {
                Accepted = true,
                Loan = _mapper.Map<LoanDto>(loan)
            };
        }
    }
}

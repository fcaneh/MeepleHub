using MediatR;
using MeepleHub.Domain.Enums;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Loans.DeclineLoan
{
    public class DeclineLoanCommandHandler : IRequestHandler<DeclineLoanCommand, DeclineLoanResponse>
    {
        private readonly ILoanRepository _loanRepository;
        
        public DeclineLoanCommandHandler(ILoanRepository loanRepository)        {
            _loanRepository = loanRepository;
        }

        public async Task<DeclineLoanResponse> Handle(DeclineLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanInvolvingUserAsync(request.UserId, request.LoanId);

            if (loan is null)
            {
                return new DeclineLoanResponse { Declined = false };
            }


            if (loan.UserGame?.UserId != request.UserId)
            {
                return new DeclineLoanResponse { Declined = false };
            }

            if (loan.Status != LoanStatus.Pending)
            {
                return new DeclineLoanResponse { Declined = false };
            }

            loan.Status = LoanStatus.Declined;
            await _loanRepository.SaveChangesAsync();

            return new DeclineLoanResponse
            {
                Declined = true
            };
        }
    }
}

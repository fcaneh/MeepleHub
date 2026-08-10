using MediatR;
using MeepleHub.Domain.Enums;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Loans.CancelLoan
{
    public class CancelLoanCommandHandler : IRequestHandler<CancelLoanCommand, CancelLoanResponse>
    {
        private readonly ILoanRepository _loanRepository;

        public CancelLoanCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<CancelLoanResponse> Handle(CancelLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanInvolvingUserAsync(request.BorrowerId, request.LoanId);

            if (loan is null)
            {
                return new CancelLoanResponse { Cancelled = false };
            }


            if (loan.BorrowerUserId != request.BorrowerId)
            {
                return new CancelLoanResponse { Cancelled = false };
            }

            if (loan.Status != LoanStatus.Pending)
            {
                return new CancelLoanResponse { Cancelled = false };
            }

            loan.Status = LoanStatus.Cancelled;
            await _loanRepository.SaveChangesAsync();

            return new CancelLoanResponse
            {
                Cancelled = true
            };
        }
    }
}

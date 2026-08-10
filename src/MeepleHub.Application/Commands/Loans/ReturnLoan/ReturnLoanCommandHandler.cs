using MediatR;
using MeepleHub.Domain.Enums;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Loans.ReturnLoan
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand, ReturnLoanResponse>
    {
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<ReturnLoanResponse> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanInvolvingUserAsync(request.UserId, request.LoanId);

            if (loan is null)
            {
                return new ReturnLoanResponse { Returned = false };
            }

            if (loan.ReturnedAt is not null)
            {
                return new ReturnLoanResponse { Returned = false };
            }

            if (loan.Status != LoanStatus.Accepted)
            {
                return new ReturnLoanResponse { Returned = false };
            }

            loan.Status = LoanStatus.Returned;
            loan.ReturnedAt = DateTime.UtcNow;
            if (request.Notes is not null)
            {
                loan.Notes = request.Notes;
            }

            await _loanRepository.SaveChangesAsync();
            return new ReturnLoanResponse
            {
                Returned = true,
            };
        }
    }
}

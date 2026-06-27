using MediatR;
using MeepleHub.Application.Commands.Loans.AcceptLoan;
using MeepleHub.Application.Commands.Loans.CancelLoan;
using MeepleHub.Application.Commands.Loans.CreateLoan;
using MeepleHub.Application.Commands.Loans.DeclineLoan;
using MeepleHub.Application.Commands.Loans.ReturnLoan;
using MeepleHub.Application.Queries.Loans.GetUserGameLoans;
using MeepleHub.Application.Queries.Loans.GetUserLoans;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/users/{userId:int}")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("loans")]
        public async Task<ActionResult<GetUserLoansResponse>> GetUserLoans(int userId)
        {
            var query = new GetUserLoansQuery { UserId = userId, };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("games/{userGameId:int}/loans")]
        public async Task<ActionResult<GetUserGameLoansResponse>> GetUserGameLoans(int userId, int userGameId)
        {
            var query = new GetUserGameLoansQuery
            {
                UserId = userId,
                UserGameId = userGameId
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("games/{userGameId:int}/loans")]
        public async Task<ActionResult<CreateLoanResponse>> CreateLoan(int userId, int userGameId, CreateLoanCommand command)
        {
            command.UserId = userId;
            command.UserGameId = userGameId;

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUserGameLoans), new { userId = userId, userGameId = userGameId }, result);
        }

        [HttpPut("loans/{loanId:int}/accept")]
        public async Task<ActionResult<AcceptLoanResponse>> AcceptLoan(int userId, int loanId, AcceptLoanCommand command)
        {
            command.UserId = userId;
            command.LoanId = loanId;

            var result = await _mediator.Send(command);

            if (result.Accepted == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/decline")]
        public async Task<ActionResult<DeclineLoanResponse>> DeclineLoan(int userId, int loanId, DeclineLoanCommand command)
        {
            command.UserId = userId;
            command.LoanId = loanId;

            var result = await _mediator.Send(command);

            if (result.Declined == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/cancel")]
        public async Task<ActionResult<CancelLoanResponse>> CancelLoan(int userId, int loanId, CancelLoanCommand command)
        {
            command.BorrowerId = userId;
            command.LoanId = loanId;

            var result = await _mediator.Send(command);

            if (result.Cancelled == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/return")]
        public async Task<ActionResult<ReturnLoanResponse>> ReturnLoan(int userId, int loanId, ReturnLoanCommand command)
        {
            command.UserId = userId;
            command.LoanId = loanId;

            var result = await _mediator.Send(command);

            if (!result.Returned)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}

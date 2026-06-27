using MediatR;
using MeepleHub.Application.Commands.Loans.AcceptLoan;
using MeepleHub.Application.Commands.Loans.CancelLoan;
using MeepleHub.Application.Commands.Loans.CreateLoan;
using MeepleHub.Application.Commands.Loans.DeclineLoan;
using MeepleHub.Application.Commands.Loans.ReturnLoan;
using MeepleHub.Application.Queries.Loans.GetUserGameLoans;
using MeepleHub.Application.Queries.Loans.GetUserLoans;
using Microsoft.AspNetCore.Mvc;
using MeepleHub.Api.Requests.Loans;

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
        public async Task<ActionResult<CreateLoanResponse>> CreateLoan([FromRoute] int userId, [FromRoute] int userGameId, [FromBody] CreateLoanRequest request)
        {
            var command = new CreateLoanCommand
            {
                UserId = userId,
                UserGameId = userGameId,
                BorrowerUserId = request.BorrowerUserId,
                ExpectedReturnDate = request.ExpectedReturnDate,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUserGameLoans), new { userId = userId, userGameId = userGameId }, result);
        }

        [HttpPut("loans/{loanId:int}/accept")]
        public async Task<ActionResult<AcceptLoanResponse>> AcceptLoan([FromRoute] int userId, [FromRoute] int loanId, [FromBody] AcceptLoanRequest request)
        {
            var command = new AcceptLoanCommand
            {
                UserId = userId,
                LoanId = loanId,
                StartDate = request.StartDate,
                ExpectedReturnDate = request.ExpectedReturnDate
            };

            var result = await _mediator.Send(command);

            if (result.Accepted == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/decline")]
        public async Task<ActionResult<DeclineLoanResponse>> DeclineLoan([FromRoute] int userId, [FromRoute] int loanId)
        {
            var command = new DeclineLoanCommand
            {
                UserId = userId,
                LoanId = loanId
            };
            

            var result = await _mediator.Send(command);

            if (result.Declined == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/cancel")]
        public async Task<ActionResult<CancelLoanResponse>> CancelLoan([FromRoute] int userId, [FromRoute] int loanId)
        {
            var command = new CancelLoanCommand
            {
                BorrowerId = userId,
                LoanId = loanId
            };

            var result = await _mediator.Send(command);

            if (result.Cancelled == false) return NotFound(result);

            return Ok(result);
        }

        [HttpPut("loans/{loanId:int}/return")]
        public async Task<ActionResult<ReturnLoanResponse>> ReturnLoan([FromRoute] int userId, [FromRoute] int loanId, [FromBody] ReturnLoanRequest request)
        {
            var command = new ReturnLoanCommand
            {
                UserId = userId,
                LoanId = loanId,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);

            if (!result.Returned)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}

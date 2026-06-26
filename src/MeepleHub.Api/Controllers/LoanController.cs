using MediatR;
using MeepleHub.Application.Commands.Loans.CreateLoan;
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
    }
}

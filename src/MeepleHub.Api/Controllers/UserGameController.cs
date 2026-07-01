using MediatR;
using MeepleHub.Application.Commands.UserGames.CreateUserGame;
using MeepleHub.Application.Commands.UserGames.DeleteUserGame;
using MeepleHub.Application.Commands.UserGames.UpdateUserGame;
using MeepleHub.Application.Queries.UserGames.GetUserGameById;
using MeepleHub.Application.Queries.UserGames.GetUserGames;
using MeepleHub.Api.Requests.UserGames;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/users/{userId:int}/games")]
    [ApiController]
    public class UserGameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserGameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserGamesResponse>> GetUserGamesByUser([FromRoute] int userId)
        {
            var query = new GetUserGamesQuery { UserId = userId };
            var result = await _mediator.Send(query);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{userGameId:int}")]
        public async Task<ActionResult<GetUserGameByIdResponse>> GetUserGameById([FromRoute] int userId, [FromRoute] int userGameId)
        {
            var query = new GetUserGameByIdQuery { UserId = userId, UserGameId = userGameId };
            var result = await _mediator.Send(query);

            if (result.UserGame is null) return NotFound(); 
            
            return Ok(result);
        }

        [HttpPut("{userGameId:int}")]
        public async Task<ActionResult<UpdateUserGameResponse>> UpdateUserGame([FromRoute] int userId, [FromRoute] int userGameId, [FromBody] UpdateUserGameRequest request)
        {
            var command = new UpdateUserGameCommand
            {
                Id = userGameId,
                UserId = userId,
                Status = request.Status,
                Condition = request.Condition,
                PurchasePrice = request.PurchasePrice,
                SellingPrice = request.SellingPrice,
                PersonalRating = request.PersonalRating,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);

            if(!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{userGameId:int}")]
        public async Task<IActionResult> DeleteUserGame([FromRoute] int userId, [FromRoute] int userGameId)
        {
            var result = await _mediator.Send(new DeleteUserGameCommand { Id = userGameId, UserId = userId });
            if (!result.Deleted) return NotFound(); 
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserGameResponse>> CreateUserGame([FromRoute] int userId, [FromBody] CreateUserGameRequest request)
        {
            var command = new CreateUserGameCommand
            {
                UserId = userId,
                GameId = request.GameId,
                Status = request.Status,
                Condition = request.Condition,
                PurchasePrice = request.PurchasePrice,
                SellingPrice = request.SellingPrice,
                PersonalRating = request.PersonalRating,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserGameById), new { userId = userId, userGameId = result.UserGame.Id }, result);
        }
    }
}

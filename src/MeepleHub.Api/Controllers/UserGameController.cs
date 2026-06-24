using MediatR;
using MeepleHub.Application.Commands.UserGames.CreateUserGame;
using MeepleHub.Application.Commands.UserGames.DeleteUserGame;
using MeepleHub.Application.Commands.UserGames.UpdateUserGame;
using MeepleHub.Application.Queries.UserGames.GetUserGameById;
using MeepleHub.Application.Queries.UserGames.GetUserGames;
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
        public async Task<ActionResult<GetUserGamesResponse>> GetUserGamesByUser(int userId)
        {
            var query = new GetUserGamesQuery { UserId = userId };
            var result = await _mediator.Send(query);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{userGameId:int}")]
        public async Task<ActionResult<GetUserGameByIdResponse>> GetUserGameById(int userId, int userGameId)
        {
            var query = new GetUserGameByIdQuery { UserGameId = userGameId };
            var result = await _mediator.Send(query);

            if (result.UserGame is null) return NotFound(); 
            
            return Ok(result);
        }

        [HttpPut("{userGameId:int}")]
        public async Task<ActionResult<UpdateUserGameResponse>> UpdateUserGame(int userId, int userGameId, UpdateUserGameCommand command)
        {
            command.Id = userGameId;
            command.UserId = userId;
            var result = await _mediator.Send(command);

            if(!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{userGameId:int}")]
        public async Task<IActionResult> DeleteUserGame(int userId, int userGameId)
        {
            var result = await _mediator.Send(new DeleteUserGameCommand { Id = userGameId, UserId = userId });
            if (!result.Deleted) return NotFound(); 
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserGameResponse>> CreateUserGame(int userId, CreateUserGameCommand command)
        {
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserGameById), new { userId = userId, userGameId = result.UserGame.Id }, result);
        }
    }
}

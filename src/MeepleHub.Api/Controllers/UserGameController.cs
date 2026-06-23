using MediatR;
using MeepleHub.Application.Commands.UserGames.CreateUserGame;
using MeepleHub.Application.Commands.UserGames.DeleteUserGame;
using MeepleHub.Application.Commands.UserGames.UpdateUserGame;
using MeepleHub.Application.Queries.UserGames.GetUserGameById;
using MeepleHub.Application.Queries.UserGames.GetUserGames;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/usergames")]
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserGameByIdResponse>> GetUserGameById(int userGameId)
        {
            var query = new GetUserGameByIdQuery { UserGameId = userGameId };
            var result = await _mediator.Send(query);

            if (result.UserGame is null) return NotFound(); 
            
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateUserGameResponse>> UpdateUserGame(int id, UpdateUserGameCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if(!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserGame(int id)
        {
            var result = await _mediator.Send(new DeleteUserGameCommand { Id = id });
            if (!result.Deleted) return NotFound(); 
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserGameResponse>> CreateUserGame(CreateUserGameCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserGameById), new { id = result.UserGame.Id }, result);
        }
    }
}

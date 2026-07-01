using MediatR;
using MeepleHub.Application.Commands.Games.CreateGame;
using MeepleHub.Application.Commands.Games.DeleteGame;
using MeepleHub.Application.Commands.Games.UpdateGame;
using MeepleHub.Application.Queries.Games.GameSearch;
using MeepleHub.Application.Queries.Games.GetGameById;
using MeepleHub.Application.Queries.Games.GetGames;
using MeepleHub.Api.Requests.Games;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/games")]
    [ApiController]

    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetGamesResponse>> GetGames([FromQuery] string? nameFilter = null)
        {
            var query = new GetGamesQuery { NameFilter = nameFilter};
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetGameByIdResponse>> GetGameById([FromRoute] int id)
        {
            var query = new GetGameByIdQuery { Id = id };
            var result = await _mediator.Send(query); 
            
            if (result.Game is null) return NotFound();

            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult<CreateGameResponse>> CreateGame([FromBody] CreateGameRequest request)
        {
            var command = new CreateGameCommand
            {
                Name = request.Name,
                RetailPrice = request.RetailPrice,
                ImageUrl = request.ImageUrl,
                PublisherId = request.PublisherId,
                Description = request.Description,
                PublishedYear = request.PublishedYear,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                MinPlayTime = request.MinPlayTime,
                MaxPlayTime = request.MaxPlayTime,
                MinAge = request.MinAge,
                Complexity = request.Complexity
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetGameById), new { id = result.Game.Id }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteGameCommand {Id = id});

            if(!result.Deleted) return NotFound();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateGameResponse>> UpdateGame([FromRoute] int id, [FromBody] UpdateGameRequest request)
        {
            var command = new UpdateGameCommand
            {
                Id = id,
                Name = request.Name,
                RetailPrice = request.RetailPrice,
                ImageUrl = request.ImageUrl,
                PublisherId = request.PublisherId,
                Description = request.Description,
                PublishedYear = request.PublishedYear,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                MinPlayTime = request.MinPlayTime,
                MaxPlayTime = request.MaxPlayTime,
                MinAge = request.MinAge,
                Complexity = request.Complexity
            };

            var result = await _mediator.Send(command);

            if (result.NameAlreadyExists) return Conflict(result);

            if(!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<GameSearchResponse>> SearchGame([FromQuery] string query)
        {
            var searchQuery = new GameSearchQuery { Query = query};
            var result = await _mediator.Send(searchQuery);

            return Ok(result);
        }
    }
}

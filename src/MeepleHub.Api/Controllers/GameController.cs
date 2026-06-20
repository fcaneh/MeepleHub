using MediatR;
using MeepleHub.Application.Queries.Games.GetGames;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MeepleHub.Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}

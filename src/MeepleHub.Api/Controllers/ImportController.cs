using MediatR;
using MeepleHub.Application.Commands.Imports.ImportBGGGame;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/imports")]
    [ApiController]

    public class ImportController : Controller
    {
        private readonly IMediator _mediator;

        public ImportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("bgg/{bggId:int}")]
        public async Task<ActionResult<ImportBggGameResponse>> ImportBggGame(int bggId)
        {
            var command = new ImportBggGameCommand { BggId = bggId };
            var result = await _mediator.Send(command);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}

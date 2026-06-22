using MediatR;
using MeepleHub.Application.Commands.Users.CreateUserCommand;
using MeepleHub.Application.Commands.Users.DeleteUserCommand;
using MeepleHub.Application.Commands.Users.UpdateUserCommand;
using MeepleHub.Application.Queries.Games.Users.GetUserById;
using MeepleHub.Application.Queries.Games.Users.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            var query = new GetUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.User is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.User.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(int id, UpdateUserCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (result.EmailAlreadyExists) return Conflict(result);
            if (!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id) 
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            if(!result.Deleted) return NotFound();
            return NoContent();
        }
    }
}

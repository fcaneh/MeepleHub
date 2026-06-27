using MediatR;
using MeepleHub.Application.Commands.Users.CreateUser;
using MeepleHub.Application.Commands.Users.DeleteUser;
using MeepleHub.Application.Commands.Users.UpdateUser;
using MeepleHub.Application.Queries.Users.GetUserById;
using MeepleHub.Application.Queries.Users.GetUsers;
using MeepleHub.Api.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace MeepleHub.Api.Controllers
{
    [Route("api/users")]
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
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById([FromRoute] int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.User is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand
            {
                Name = request.Name,
                Email = request.Email
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.User.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            var command = new UpdateUserCommand
            {
                Id = id,
                Name = request.Name,
                Email = request.Email
            };

            var result = await _mediator.Send(command);

            if (result.EmailAlreadyExists) return Conflict(result);
            if (!result.Updated) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id) 
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            if(!result.Deleted) return NotFound();
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Users.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                return new DeleteUserResponse { Deleted = false };
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return new DeleteUserResponse { Deleted = true };
        }
    }
}

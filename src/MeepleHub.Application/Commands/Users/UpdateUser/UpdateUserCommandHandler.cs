using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user is null)
            {
                return new UpdateUserResponse { Updated = false };
            }

            var userEmailAlreadyExists = await _userRepository.ExistsByEmailAsync(request.Email, request.Id);
            if (userEmailAlreadyExists) 
            {
                return new UpdateUserResponse { EmailAlreadyExists = true };
            }

            user.Name = request.Name;
            user.Email = request.Email;

            await _userRepository.SaveChangesAsync();
            return new UpdateUserResponse { Updated =  true , User = _mapper.Map<UserDto>(user)}; 
        }
    }
}

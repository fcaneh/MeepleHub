using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Users.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEmailAlreadyExists = await _userRepository.ExistsByEmailAsync(request.Email);
            if (userEmailAlreadyExists)
            {
                throw new InvalidOperationException("Another User with the same email already exists");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new CreateUserResponse
            {
                User = _mapper.Map<UserDto>(user)
            };

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Games.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            return new GetUserByIdResponse
            {
                User = user is null ? null : _mapper.Map<UserDto>(user)
            };
        }
    }
}

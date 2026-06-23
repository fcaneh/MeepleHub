using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.UserGames.GetUserGameById
{
    public class GetUserGameByIdQueryHandler : IRequestHandler<GetUserGameByIdQuery, GetUserGameByIdResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;

        public GetUserGameByIdQueryHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
        }

        public async Task<GetUserGameByIdResponse> Handle(GetUserGameByIdQuery request, CancellationToken cancellationToken)
        {
            var userGame = await _userGameRepository.GetByIdAsync(request.UserGameId);
            return new GetUserGameByIdResponse
            {
                UserGame = _mapper.Map<UserGameDto>(userGame)
            };
        }
    }
}

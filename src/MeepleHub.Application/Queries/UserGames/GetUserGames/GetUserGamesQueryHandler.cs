using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.UserGames.GetUserGames
{
    public class GetUserGamesQueryHandler : IRequestHandler<GetUserGamesQuery, GetUserGamesResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;

        public GetUserGamesQueryHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
        }

        public async Task<GetUserGamesResponse> Handle(GetUserGamesQuery request, CancellationToken cancellationToken)
        {
            var userGames = await _userGameRepository.GetAllAsync(request.UserId);
            return new GetUserGamesResponse
            {
                UserGames = _mapper.Map<IEnumerable<UserGameDto>>(userGames)
            };
        }
    }
}

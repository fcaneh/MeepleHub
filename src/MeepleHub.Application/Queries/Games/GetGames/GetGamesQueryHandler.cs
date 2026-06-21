using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Games.GetGames
{
    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GetGamesResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GetGamesResponse> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.NameFilter))
            {
                games = games.Where(game => game.Name.Contains(request.NameFilter));
            }
            return new GetGamesResponse
            {
                Games = _mapper.Map<IEnumerable<GameDto>>(games)
            };
        }
    }
}

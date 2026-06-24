using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Games.GameSearch
{
    public class GameSearchQueryHandler : IRequestHandler<GameSearchQuery, GameSearchResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameSearchQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameSearchResponse> Handle(GameSearchQuery request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.SearchAsync(request.Query);

            return new GameSearchResponse
            {
                Games = _mapper.Map<IEnumerable<GameDto>>(games)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Games.GameSearch
{
    public class GameSearchQueryHandler : IRequestHandler<GameSearchQuery, GameSearchResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IBggClient _bggClient;
        private readonly IBggGameParser _bggGameParser;

        public GameSearchQueryHandler(IGameRepository gameRepository, IMapper mapper, IBggClient bggClient, IBggGameParser bggGameParser)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _bggClient = bggClient;
            _bggGameParser = bggGameParser;
        }

        public async Task<GameSearchResponse> Handle(GameSearchQuery request, CancellationToken cancellationToken)
        {
            var games = (await _gameRepository.SearchAsync(request.Query)).ToList();

            if (games.Any())
            {
                return new GameSearchResponse
                {
                    LocalGames = _mapper.Map<IEnumerable<GameDto>>(games),
                    HasLocalResults = true
                };
            }

            var bggSearchResponse = await _bggClient.SearchGamesXmlAsync(request.Query);

            if(!bggSearchResponse.Success)
            {
                return new GameSearchResponse
                {
                    HasLocalResults = false,
                    ErrorMessage = bggSearchResponse.ErrorMessage ?? "Unable to search BGG data."
                };
            }

            if (string.IsNullOrWhiteSpace(bggSearchResponse.Xml))
            {
                return new GameSearchResponse
                {
                    HasLocalResults = false,
                    ErrorMessage = "BGG returned an empty search response."
                };
            }

            var externalResults = _bggGameParser.ParseSearchXml(bggSearchResponse.Xml);

            return new GameSearchResponse
            {
                HasLocalResults = false,
                ExternalGames = externalResults
            };
        }
    }
}

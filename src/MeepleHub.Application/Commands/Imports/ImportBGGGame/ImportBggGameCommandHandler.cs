using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Imports.ImportBGGGame
{
    public class ImportBggGameCommandHandler : IRequestHandler<ImportBggGameCommand, ImportBggGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IBggClient _bggClient;
        private readonly IBggGameParser _bggGameParser;
        private readonly IPublisherRepository _publisherRepository;

        public ImportBggGameCommandHandler(IGameRepository gameRepository, IMapper mapper, IBggClient bggClient, IBggGameParser bggGameParser, IPublisherRepository publisherRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _bggClient = bggClient;
            _bggGameParser = bggGameParser;
            _publisherRepository = publisherRepository;
        }

        public async Task<ImportBggGameResponse> Handle(ImportBggGameCommand request, CancellationToken cancellationToken)
        {
            var existingGame = await _gameRepository.FindByExternalReferenceAsync("BGG", request.BggId.ToString());

            if (existingGame is not null)
            {
                return new ImportBggGameResponse
                {
                    AlreadyExists = true,
                    Game = _mapper.Map<GameDto>(existingGame)
                };
            }

            var xml = await _bggClient.GetThingXmlByIdAsync(request.BggId);

            if (xml is null)
            {
                return new ImportBggGameResponse
                {
                    Imported = false
                };
            }

            var gameData = _bggGameParser.ParseThingXml(xml);

            if (gameData is null)
            {
                return new ImportBggGameResponse
                {
                    Imported = false
                };
            }

            var publisherName = string.IsNullOrWhiteSpace(gameData.PublisherName) ? "Unknown Publisher" : gameData.PublisherName;
            var publisher = await _publisherRepository.GetOrCreateAsync(publisherName);

            var game = new Game
            {
                Name = gameData.Name,
                Publisher = publisher,
                Complexity = gameData.Complexity,
                Description = gameData.Description,
                ExternalReferences = new List<GameExternalReference>
                {
                    new GameExternalReference
                    {
                        Source = "BGG",
                        ExternalId = request.BggId.ToString()
                    }
                },
                ImageUrl = gameData.ImageUrl,
                MinPlayers = gameData.MinPlayers,
                MaxPlayers = gameData.MaxPlayers,
                MinPlayTime = gameData.MinPlayTime,
                MaxPlayTime = gameData.MaxPlayTime,
                MinAge = gameData.MinAge,
                PublishedYear = gameData.PublishedYear,
                RetailPrice = 0m,
                Aliases = gameData.Aliases.Select(alias => new GameAlias { Name = alias }).ToList(),
            };

            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();

            return new ImportBggGameResponse 
            {
                Game = _mapper.Map<GameDto>(game),
                Imported = true
            };
        }
    }
}

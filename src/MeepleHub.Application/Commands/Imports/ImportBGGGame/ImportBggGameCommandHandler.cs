using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Imports.ImportBGGGame
{
    public class ImportBggGameCommandHandler : IRequestHandler<ImportBggGameCommand, ImportBggGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IBggClient _bggClient;

        public ImportBggGameCommandHandler(IGameRepository gameRepository, IMapper mapper, IBggClient bggClient)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _bggClient = bggClient;
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

            return new ImportBggGameResponse 
            { 
                Imported = false 
            };
        }
    }
}

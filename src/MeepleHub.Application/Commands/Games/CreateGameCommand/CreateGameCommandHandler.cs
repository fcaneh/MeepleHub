using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Games.CreateGameCommand
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<CreateGameResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var gameAlreadyExists = await _gameRepository.ExistsByNameAsync(request.Name);

            if (gameAlreadyExists)
            {
                throw new InvalidOperationException("A game with this name already exists");
            }

            var game = new Game
            {
                Name = request.Name,
                RetailPrice = request.RetailPrice,
                ImageUrl = request.ImageUrl,
                PublisherId = request.PublisherId,
                Description = request.Description,
                PublishedYear = request.PublishedYear,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                MinPlayTime = request.MinPlayTime,
                MaxPlayTime = request.MaxPlayTime,
                MinAge = request.MinAge,
                Complexity = request.Complexity
            };

            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();

            return new CreateGameResponse
            {
                Game = _mapper.Map<GameDto>(game),
            };

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Games.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, UpdateGameResponse>
    {   
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<UpdateGameResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.Id);
            if (game is null)
            {
                return new UpdateGameResponse { Updated = false };
            }

            var gameNameAlreadyExists = await _gameRepository.ExistsByNameAsync(request.Name, request.Id);
            if (gameNameAlreadyExists)
            {
                return new UpdateGameResponse { NameAlreadyExists = true };
            }

            game.Name = request.Name;
            game.Description = request.Description;
            game.ImageUrl = request.ImageUrl;
            game.PublishedYear = request.PublishedYear;
            game.MinPlayers = request.MinPlayers;
            game.MaxPlayers = request.MaxPlayers;
            game.MinPlayTime = request.MinPlayTime;
            game.MaxPlayTime = request.MaxPlayTime;
            game.MinAge = request.MinAge;
            game.Complexity = request.Complexity;
            game.RetailPrice = request.RetailPrice;
            game.PublisherId = request.PublisherId;

            await _gameRepository.SaveChangesAsync();
            return new UpdateGameResponse { Updated = true, Game = _mapper.Map<GameDto>(game) };
        }
    }
}

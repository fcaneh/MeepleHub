using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Application.Common.Exceptions;

namespace MeepleHub.Application.Commands.UserGames.CreateUserGame
{
    public class CreateUserGameCommandHandler : IRequestHandler<CreateUserGameCommand, CreateUserGameResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public CreateUserGameCommandHandler(IUserGameRepository userGameRepository, IMapper mapper, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        public async Task<CreateUserGameResponse> Handle(CreateUserGameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                throw new NotFoundException("User", request.UserId);
            }

            var game = await _gameRepository.GetByIdAsync(request.GameId);
            if (game is null)
            {
                throw new NotFoundException("Game", request.GameId);
            }

            var userGame = new UserGame
            {
                UserId = request.UserId,
                GameId = request.GameId,
                Status = request.Status,
                Condition = request.Condition,
                PersonalRating = request.PersonalRating,
                PurchasePrice = request.PurchasePrice,
                Notes = request.Notes,
                SellingPrice = request.SellingPrice,
            };

            await _userGameRepository.AddAsync(userGame);
            await _userGameRepository.SaveChangesAsync();
            return new CreateUserGameResponse
            {
                UserGame = _mapper.Map<UserGameDto>(userGame)
            };
        }
    }
}

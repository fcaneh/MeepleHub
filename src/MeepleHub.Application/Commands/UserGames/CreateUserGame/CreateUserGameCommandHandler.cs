using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.UserGames.CreateUserGame
{
    public class CreateUserGameCommandHandler : IRequestHandler<CreateUserGameCommand, CreateUserGameResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;

        public CreateUserGameCommandHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserGameResponse> Handle(CreateUserGameCommand request, CancellationToken cancellationToken)
        {
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

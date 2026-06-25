using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.UserGames.UpdateUserGame
{
    public class UpdateUserGameCommandHandler : IRequestHandler<UpdateUserGameCommand, UpdateUserGameResponse>
    {   
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;

        public UpdateUserGameCommandHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
        }

        public async Task<UpdateUserGameResponse> Handle(UpdateUserGameCommand request, CancellationToken cancellationToken)
        {
            var userGame = await _userGameRepository.GetByIdAsync(request.UserId, request.Id);

            if (userGame is null)
            {
                return new UpdateUserGameResponse { Updated = false};
            }

            userGame.Status = request.Status;
            userGame.Condition = request.Condition;
            userGame.PersonalRating = request.PersonalRating;
            userGame.PurchasePrice = request.PurchasePrice;
            userGame.Notes = request.Notes;
            userGame.SellingPrice = request.SellingPrice;

            await _userGameRepository.SaveChangesAsync();
            return new UpdateUserGameResponse
            {
                Updated = true, UserGame = _mapper.Map<UserGameDto>(userGame)
            };
        }
    }
}

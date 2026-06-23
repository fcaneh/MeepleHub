using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.Commands.Games.DeleteGame;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.UserGames.DeleteUserGame
{
    public class DeleteUserGameCommandHandler : IRequestHandler<DeleteUserGameCommand, DeleteUserGameResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IMapper _mapper;

        public DeleteUserGameCommandHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
            _mapper = mapper;
        }
        public async Task<DeleteUserGameResponse> Handle(DeleteUserGameCommand request, CancellationToken cancellationToken)
        {
            var userGame = await _userGameRepository.GetByIdAsync(request.Id);

            if (userGame is null) { return new DeleteUserGameResponse { Deleted = false }; } 

            _userGameRepository.Delete(userGame);  
            await _userGameRepository.SaveChangesAsync();
            return new DeleteUserGameResponse { Deleted = true };
        }
    }
}

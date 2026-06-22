using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.Games.DeleteGameCommand
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, DeleteGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        
        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }


        public async Task<DeleteGameResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.Id);
            if (game is null)
            {
                return new DeleteGameResponse { Deleted = false };
            }

            _gameRepository.Delete(game);
            await _gameRepository.SaveChangesAsync();

            return new DeleteGameResponse { Deleted = true };

        }
    }
}

using AutoMapper;
using MediatR;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Commands.UserGames.DeleteUserGame
{
    public class DeleteUserGameCommandHandler : IRequestHandler<DeleteUserGameCommand, DeleteUserGameResponse>
    {
        private readonly IUserGameRepository _userGameRepository;
        
        public DeleteUserGameCommandHandler(IUserGameRepository userGameRepository, IMapper mapper)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<DeleteUserGameResponse> Handle(DeleteUserGameCommand request, CancellationToken cancellationToken)
        {
            var userGame = await _userGameRepository.GetByIdAsync(request.Id);

            if (userGame is null) 
            { 
                return new DeleteUserGameResponse { Deleted = false }; 
            }

            if (userGame.UserId != request.UserId)
            {
                return new DeleteUserGameResponse { Deleted = false };
            }

            _userGameRepository.Delete(userGame);  
            await _userGameRepository.SaveChangesAsync();
            return new DeleteUserGameResponse { Deleted = true };
        }
    }
}

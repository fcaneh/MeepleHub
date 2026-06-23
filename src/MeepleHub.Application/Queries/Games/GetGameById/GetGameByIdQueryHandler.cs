using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Interfaces;

namespace MeepleHub.Application.Queries.Games.GetGameById
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GetGameByIdResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GetGameByIdResponse> Handle(GetGameByIdQuery request,  CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.Id);

            return new GetGameByIdResponse 
            { 
                Game = game is null ? null : _mapper.Map<GameDto>(game) 
            };
        }
    }
}

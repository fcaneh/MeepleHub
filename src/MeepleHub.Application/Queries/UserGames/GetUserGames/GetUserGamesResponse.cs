using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Application.Queries.UserGames.GetUserGames
{
    public class GetUserGamesResponse
    {
        public IEnumerable<UserGameDto> UserGames {  get; set; } = Enumerable.Empty<UserGameDto>();
    }
}

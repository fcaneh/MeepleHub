using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.UserGames.GetUserGameById
{
    public class GetUserGameByIdResponse
    {
        public UserGameDto? UserGame { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.UserGames.CreateUserGame
{
    public class CreateUserGameResponse
    {
        public UserGameDto UserGame { get; set; } = null!;
    }
}

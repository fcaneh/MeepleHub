using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.UserGames.UpdateUserGame
{
    public class UpdateUserGameResponse
    {
        public bool Updated { get; set; }
        public UserGameDto? UserGame { get; set; }
    }
}

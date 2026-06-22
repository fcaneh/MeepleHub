using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Games.CreateGameCommand
{
    public class CreateGameResponse
    {
        public GameDto Game { get; set; } = null!;
    }
}

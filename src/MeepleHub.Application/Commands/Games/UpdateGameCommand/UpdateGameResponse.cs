using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Games.UpdateGameCommand
{
    public class UpdateGameResponse
    {
        public bool Updated { get; set; }
        public bool NameAlreadyExists { get; set; }
        public GameDto? Game { get; set; }
    }
}

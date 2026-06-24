using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Commands.Imports.ImportBGGGame
{
    public class ImportBggGameResponse
    {
        public GameDto Game { get; set; } = null!;
        public bool AlreadyExists { get; set; }
        public bool Imported { get; set; }
    }
}

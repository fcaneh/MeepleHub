using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Commands.Imports.ImportBGGGame
{
    public class ImportBggGameCommand :IRequest<ImportBggGameResponse>
    {
        public int BggId { get; set; }
    }
}

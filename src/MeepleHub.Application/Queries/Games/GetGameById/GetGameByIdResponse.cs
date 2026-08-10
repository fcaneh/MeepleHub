using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.DTOs;

namespace MeepleHub.Application.Queries.Games.GetGameById
{
    public class GetGameByIdResponse
    {
        public GameDto? Game { get; set; }
    }
}

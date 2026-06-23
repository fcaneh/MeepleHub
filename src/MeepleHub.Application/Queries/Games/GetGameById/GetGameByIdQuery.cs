using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MeepleHub.Application.Queries.Games.GetGameById
{
    public class GetGameByIdQuery : IRequest<GetGameByIdResponse>
    {
        public int Id { get; set; }
    }
}

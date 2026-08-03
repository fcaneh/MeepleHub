using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.ExternalModels.Bgg;

namespace MeepleHub.Application.ExternalInterfaces
{
    public interface IBggClient
    {
        Task<BggClientResult> GetThingXmlByIdAsync(int bggId);
        Task<BggClientResult> SearchGamesXmlAsync(string query);

    }
}

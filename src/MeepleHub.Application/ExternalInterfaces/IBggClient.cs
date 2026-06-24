using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.ExternalInterfaces
{
    public interface IBggClient
    {
        Task<string?> GetThingXmlByIdAsync(int bggId);
    }
}

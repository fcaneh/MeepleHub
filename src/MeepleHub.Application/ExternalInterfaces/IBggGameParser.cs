using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.ExternalModels.Bgg;

namespace MeepleHub.Application.ExternalInterfaces
{
    public interface IBggGameParser
    {
        BggGameImportData? ParseThingXml(string xml);
    }
}

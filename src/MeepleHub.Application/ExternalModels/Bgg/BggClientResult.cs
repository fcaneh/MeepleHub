using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.ExternalModels.Bgg
{
    public class BggClientResult
    {
        public bool Success { get; set; }
        public string? Xml { get; set; }
        public int? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

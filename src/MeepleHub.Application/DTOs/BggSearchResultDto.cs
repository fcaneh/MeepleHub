using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.DTOs
{
    public class BggSearchResultDto
    {
        public int BggId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? PublishedYear { get; set; }
    }
}

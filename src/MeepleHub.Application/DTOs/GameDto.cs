using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.DTOs;

public class GameDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal RetailPrice { get; set; }
    public string? ImageUrl { get; set; }
    public int PublisherId { get; set; }
    public string? Description { get; set; }
    public int? PublishedYear { get; set; }
    public int MinimumPlayers { get; set; }
    public int MaximumPlayers { get; set; }
    public int Status { get; set; }
    public int Condition { get; set; }
    public int OwnerId { get; set; }
}

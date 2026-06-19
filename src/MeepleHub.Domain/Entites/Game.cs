using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Domain.Entites
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        public string? ImageUrl { get; set; }
        public Publisher? Publisher { get; set; }
        public int PublisherId { get; set; }
        public string? Description { get; set; }
        public int? PublishedYear { get; set; }
        public int MinimumPlayers { get; set; }
        public int MaximumPlayers { get; set; }
        public Status Status { get; set; }
        public Condition Condition { get; set; }
        public int OwnerId { get; set; }
        public User? Owner {  get; set; }
    }
}

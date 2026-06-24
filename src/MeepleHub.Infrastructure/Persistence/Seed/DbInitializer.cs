using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Enums;

namespace MeepleHub.Infrastructure.Persistence.Seed
{
    public static class DbInitializer
    {
        public static void Seed(MeepleHubDbContext meepleHubDbContext)
        {
            if (meepleHubDbContext.Users.Any()) return;

            var reposProduction = new Publisher
            {
                Name = "Repos Production"
            };

            var cmon = new Publisher
            {
                Name = "CMON"
            };

            var daysOfWonder = new Publisher
            {
                Name = "Days of Wonder"
            };

            var planB = new Publisher
            {
                Name = "Plan B Games"
            };

            var spaceCowboys = new Publisher
            {
                Name = "Space Cowboys"
            };

            meepleHubDbContext.Publishers.AddRange(reposProduction, cmon, daysOfWonder, planB, spaceCowboys);

            var fabien = new User
            {
                Name = "fabien",
                Email = "fabien@test.fr"
            };

            var alice = new User
            {
                Name = "alice",
                Email = "alice@test.fr"
            };

            var mehdi = new User
            {
                Name = "mehdi",
                Email = "mehdi@test.fr"
            };

            meepleHubDbContext.Users.AddRange(fabien, alice, mehdi);
            meepleHubDbContext.SaveChanges();

            var sevenWonders = new Game
            {
                Name = "7 Wonders",
                Description = "Card drafting civilization game.",
                RetailPrice = 39.99m,
                PublishedYear = 2010,
                MinPlayers = 2,
                MaxPlayers = 7,
                MinPlayTime = 30,
                MaxPlayTime = 60,
                MinAge = 10,
                Complexity = 2.3m,
                PublisherId = reposProduction.Id
            };

            var zombicide = new Game
            {
                Name = "Zombicide",
                Description = "Cooperative zombie survival game.",
                RetailPrice = 99.99m,
                PublishedYear = 2012,
                MinPlayers = 1,
                MaxPlayers = 6,
                MinPlayTime = 60,
                MaxPlayTime = 180,
                MinAge = 14,
                Complexity = 2.8m,
                PublisherId = cmon.Id
            };

            var ticketToRide = new Game
            {
                Name = "Ticket to Ride",
                Description = "Route-building railway game.",
                RetailPrice = 44.99m,
                PublishedYear = 2004,
                MinPlayers = 2,
                MaxPlayers = 5,
                MinPlayTime = 30,
                MaxPlayTime = 60,
                MinAge = 8,
                Complexity = 1.9m,
                PublisherId = daysOfWonder.Id
            };

            var azul = new Game
            {
                Name = "Azul",
                Description = "Tile drafting and pattern building game.",
                RetailPrice = 39.99m,
                PublishedYear = 2017,
                MinPlayers = 2,
                MaxPlayers = 4,
                MinPlayTime = 30,
                MaxPlayTime = 45,
                MinAge = 8,
                Complexity = 1.8m,
                PublisherId = planB.Id
            };

            var splendor = new Game
            {
                Name = "Splendor",
                Description = "Engine-building gem trading game.",
                RetailPrice = 34.99m,
                PublishedYear = 2014,
                MinPlayers = 2,
                MaxPlayers = 4,
                MinPlayTime = 30,
                MaxPlayTime = 30,
                MinAge = 10,
                Complexity = 1.8m,
                PublisherId = spaceCowboys.Id
            };

            meepleHubDbContext.Games.AddRange(sevenWonders, zombicide, ticketToRide, azul, splendor);
            meepleHubDbContext.SaveChanges();

            meepleHubDbContext.GameAliases.AddRange(
                new GameAlias { GameId = ticketToRide.Id, Name = "Les Aventuriers du Rail", LanguageCode = "fr" },
                new GameAlias { GameId = ticketToRide.Id, Name = "Zug um Zug", LanguageCode = "de" },
                new GameAlias { GameId = sevenWonders.Id, Name = "7 Wonders", LanguageCode = "fr" },
                new GameAlias { GameId = splendor.Id, Name = "Splendor", LanguageCode = "fr" });

            meepleHubDbContext.GameExternalReferences.AddRange(
                new GameExternalReference { GameId = sevenWonders.Id, Source = "BGG", ExternalId = "68448" },
                new GameExternalReference { GameId = zombicide.Id, Source = "BGG", ExternalId = "113924" },
                new GameExternalReference { GameId = ticketToRide.Id, Source = "BGG", ExternalId = "9209" },
                new GameExternalReference { GameId = azul.Id, Source = "BGG", ExternalId = "230802" },
                new GameExternalReference { GameId = splendor.Id, Source = "BGG", ExternalId = "148228" });

            meepleHubDbContext.UserGames.AddRange(
                new UserGame
                {
                    UserId = fabien.Id,
                    GameId = sevenWonders.Id,
                    Status = Status.Owned,
                    Condition = Condition.VeryGood,
                    PurchasePrice = 24.99m,
                    PersonalRating = 8,
                    Notes = "Sleeved."
                },
                new UserGame
                {
                    UserId = fabien.Id,
                    GameId = azul.Id,
                    Status = Status.Owned,
                    Condition = Condition.Good,
                    PurchasePrice = 20m,
                    PersonalRating = 7
                },
                new UserGame
                {
                    UserId = fabien.Id,
                    GameId = azul.Id,
                    Status = Status.ForSale,
                    Condition = Condition.New,
                    SellingPrice = 28m,
                    Notes = "Second copy, still sealed."
                },
                new UserGame
                {
                    UserId = alice.Id,
                    GameId = ticketToRide.Id,
                    Status = Status.Loaned,
                    Condition = Condition.VeryGood,
                    PersonalRating = 9,
                    Notes = "Loaned to Fabien."
                },
                new UserGame
                {
                    UserId = mehdi.Id,
                    GameId = zombicide.Id,
                    Status = Status.ForTrade,
                    Condition = Condition.Good,
                    SellingPrice = 55m
                },
                new UserGame
                {
                    UserId = mehdi.Id,
                    GameId = splendor.Id,
                    Status = Status.Wanted,
                    Condition = Condition.New,
                    Notes = "Looking for a used copy."
                });

            meepleHubDbContext.SaveChanges();
        }
    }
}

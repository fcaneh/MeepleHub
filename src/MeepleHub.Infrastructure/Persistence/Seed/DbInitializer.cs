using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Infrastructure.Persistence.Seed
{
    public static class DbInitializer
    {
        public static void Seed(MeepleHubDbContext meepleHubDbContext) 
        {
            if (meepleHubDbContext.Users.Any()) return;

            var reposProduction = new Publisher
            {
                Name = "ReposProduction Production"
            };

            var cmon = new Publisher
            {
                Name = "CMON"
            };
            
            meepleHubDbContext.Publishers.AddRange(reposProduction, cmon);

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

            meepleHubDbContext.Users.AddRange(fabien, alice);

            meepleHubDbContext.SaveChanges();

            meepleHubDbContext.Games.AddRange(
                new Game
                {
                    Name = "7 Wonders",
                    RetailPrice = 39.99m,
                    MinPlayers = 2,
                    MaxPlayers = 7,
                    MinPlayTime = 30,
                    MaxPlayTime = 60,
                    MinAge = 10,
                    Complexity = 2.3m,
                    PublisherId = reposProduction.Id
                },
                new Game
                {
                    Name = "Zombicide",
                    RetailPrice = 99.99m,
                    MinPlayers = 1,
                    MaxPlayers = 6,
                    MinPlayTime = 60,
                    MaxPlayTime = 180,
                    MinAge = 14,
                    Complexity = 2.8m,
                    PublisherId = cmon.Id
                });

            meepleHubDbContext.SaveChanges();
        }
    }
}

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
                    MinimumPlayers = 2,
                    MaximumPlayers = 7,
                    Status = Status.Owned,
                    Condition = Condition.VeryGood,
                    OwnerId = fabien.Id,
                    PublisherId = reposProduction.Id
                },
                new Game
                {
                    Name = "Zombicide",
                    RetailPrice = 99.99m,
                    MinimumPlayers = 1,
                    MaximumPlayers = 6,
                    Status = Status.ForTrade,
                    Condition = Condition.Good,
                    OwnerId = alice.Id,
                    PublisherId = cmon.Id
                });
            meepleHubDbContext.SaveChanges();
        }
    }
}

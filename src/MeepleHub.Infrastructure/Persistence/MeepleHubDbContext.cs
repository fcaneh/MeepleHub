using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeepleHub.Infrastructure.Persistence
{
    public class MeepleHubDbContext : DbContext
    {
        public MeepleHubDbContext(DbContextOptions<MeepleHubDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<UserGame> UserGames => Set<UserGame>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Designer> Designers => Set<Designer>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(game => game.RetailPrice).HasPrecision(10, 2);
            modelBuilder.Entity<Game>().HasIndex(game => game.Name).IsUnique();
            modelBuilder.Entity<Game>().Property(game => game.Complexity).HasPrecision(3, 2);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.User).WithMany(u => u.UserGames).HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.Game).WithMany(g => g.UserGames).HasForeignKey(ug => ug.GameId);
            modelBuilder.Entity<UserGame>().Property(ug => ug.PurchasePrice).HasPrecision(10, 2);
            modelBuilder.Entity<UserGame>().Property(ug => ug.SellingPrice).HasPrecision(10, 2);
        }
    }
}

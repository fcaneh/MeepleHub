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
        public DbSet<GameAlias> GameAliases => Set<GameAlias>();
        public DbSet<GameExternalReference> GameExternalReferences => Set<GameExternalReference>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Trade> Trades => Set<Trade>();
        public DbSet<TradeItem> TradeItems => Set<TradeItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(game => game.RetailPrice).HasPrecision(10, 2);
            modelBuilder.Entity<Game>().HasIndex(game => game.Name).IsUnique();
            modelBuilder.Entity<Game>().Property(game => game.Complexity).HasPrecision(3, 2);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.User).WithMany(u => u.UserGames).HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.Game).WithMany(g => g.UserGames).HasForeignKey(ug => ug.GameId);
            modelBuilder.Entity<UserGame>().Property(ug => ug.PurchasePrice).HasPrecision(10, 2);
            modelBuilder.Entity<UserGame>().Property(ug => ug.SellingPrice).HasPrecision(10, 2);
            modelBuilder.Entity<GameExternalReference>().HasIndex(reference => new { reference.Source, reference.ExternalId }).IsUnique();
            modelBuilder.Entity<GameExternalReference>().Property(reference => reference.Source).HasMaxLength(50);
            modelBuilder.Entity<GameExternalReference>().Property(reference => reference.ExternalId).HasMaxLength(100);
            modelBuilder.Entity<GameAlias>().Property(alias => alias.LanguageCode).HasMaxLength(10);
            modelBuilder.Entity<GameAlias>().Property(alias => alias.Name).HasMaxLength(300);
            modelBuilder.Entity<Loan>().HasOne(loan => loan.UserGame).WithMany(userGame => userGame.Loans).HasForeignKey(loan => loan.UserGameId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Loan>().HasOne(loan => loan.BorrowerUser).WithMany(user => user.BorrowedLoans).HasForeignKey(loan => loan.BorrowerUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Loan>().Property(loan => loan.Notes).HasMaxLength(1000);
            modelBuilder.Entity<Trade>().HasOne<User>().WithMany().HasForeignKey(trade => trade.RequesterUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Trade>().HasOne<User>().WithMany().HasForeignKey(trade => trade.ReceiverUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Trade>().Property(trade => trade.Notes).HasMaxLength(1000);
            modelBuilder.Entity<TradeItem>().HasOne(item => item.Trade).WithMany(trade => trade.TradeItems).HasForeignKey(item => item.TradeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TradeItem>().HasOne(item => item.UserGame).WithMany().HasForeignKey(item => item.UserGameId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TradeItem>().HasOne(item => item.RequestedByUser).WithMany().HasForeignKey(item => item.RequestedByUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

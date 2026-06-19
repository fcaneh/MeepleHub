using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entites;
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
        //protected MeepleHubDbContext()
        //{
        //}
    }
}

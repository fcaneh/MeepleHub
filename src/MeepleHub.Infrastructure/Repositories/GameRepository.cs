using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeepleHub.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly MeepleHubDbContext _context;
        public GameRepository(MeepleHubDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public void Delete(Game game)
        {
            _context.Games.Remove(game);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var normalizedName = name.Trim().ToLower();
            return await _context.Games.AnyAsync(game => game.Name.ToLower() == normalizedName);
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludedGameId)
        {
            var normalizedName = name.Trim().ToLower();
            return await _context.Games.AnyAsync(game => game.Id != excludedGameId && game.Name.ToLower() == normalizedName);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> SearchAsync(string query)
        {
            var normalizedQuery = query.Trim().ToLower();

            return await _context.Games.Where(game => 
                game.Name.ToLower().Contains(normalizedQuery) || 
                game.Aliases != null 
                    && game.Aliases.Any(alias => alias.Name.ToLower().Contains(normalizedQuery)))
                .ToListAsync();
        }
    }
}

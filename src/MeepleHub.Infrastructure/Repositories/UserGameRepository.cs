using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeepleHub.Infrastructure.Repositories
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly MeepleHubDbContext _context;
        
        public UserGameRepository(MeepleHubDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserGame userGame)
        {
            await _context.UserGames.AddAsync(userGame);
        }

        public void Delete(UserGame userGame)
        {
            _context.UserGames.Remove(userGame);
        }

        public async Task<IEnumerable<UserGame>> GetAllAsync(int userId)
        {
            return await _context.UserGames.Where(ug => ug.UserId == userId).ToListAsync();
        }

        public async Task<UserGame?> GetByIdAsync(int userGameId)
        {
            return await _context.UserGames.FindAsync(userGameId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

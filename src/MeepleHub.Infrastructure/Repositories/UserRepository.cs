using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeepleHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeepleHubDbContext _context;
        public UserRepository(MeepleHubDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLower();
            return await _context.Users.AnyAsync(user => user.Email.ToLower() == normalizedEmail);
        }

        public async Task<bool> ExistsByEmailAsync(string email, int excludedUserId)
        {
            var normalizedEmail = email.Trim().ToLower();

            return await _context.Users.AnyAsync(user => user.Id != excludedUserId && user.Email.ToLower() == normalizedEmail);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
    }
}

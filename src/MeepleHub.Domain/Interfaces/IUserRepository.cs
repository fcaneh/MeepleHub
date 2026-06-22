using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email, int excludedUserId);
        Task AddAsync(User user);
        Task SaveChangesAsync();
        void Delete(User user);
    }
}

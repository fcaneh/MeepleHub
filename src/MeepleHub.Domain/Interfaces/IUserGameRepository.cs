using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Domain.Interfaces
{
    public interface IUserGameRepository
    {
        Task<IEnumerable<UserGame>> GetAllAsync(int userId);
        Task<UserGame?> GetByIdAsync(int userId, int userGameId);
        Task AddAsync(UserGame userGame);
        Task SaveChangesAsync();
        void Delete(UserGame userGame);
    }
}

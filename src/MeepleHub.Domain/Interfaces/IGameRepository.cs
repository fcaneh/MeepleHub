using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
//using MeepleHub.Application.DTOs;

namespace MeepleHub.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task SaveChangesAsync();
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name, int excludedGameId);
        void Delete(Game game);
    }
}

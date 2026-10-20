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
        Task<IEnumerable<Game>> SearchAsync(string query);

        Task<Game?> FindByExternalReferenceAsync(string source, string externalId);
        Task<IEnumerable<Game>> GetFilterAsync(string? nameFilter = null, IEnumerable<int>? categoryIds = null, IEnumerable<int>? mechanicIds = null, IEnumerable<int>? publisherIds = null, IEnumerable<int>? designerIds = null, int? minPlayers = null, int? maxPlayers = null, int? numberPlayers = null, int? youngestPlayerAge = null);
    }
}

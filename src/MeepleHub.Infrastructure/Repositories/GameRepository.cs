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

        public async Task<Game?> FindByExternalReferenceAsync(string source, string externalId)
        {
            var normalizedSource = source.Trim().ToLower();
            var normalizeExternalId = externalId.Trim();

            return await _context.Games
                .FirstOrDefaultAsync(game =>
                    game.ExternalReferences != null &&
                    game.ExternalReferences.Any(reference => reference.Source.ToLower() == normalizedSource &&
                    reference.ExternalId == normalizeExternalId));

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

        public async Task<IEnumerable<Game>> GetFilterAsync(string? nameFilter = null, IEnumerable<int>? categoryIds = null,IEnumerable<int>? mechanicIds = null,IEnumerable<int>? publisherIds = null, IEnumerable<int>? designerIds = null, int? minPlayers = null, int? maxPlayers = null, int? numberPlayers = null, int? youngestPlayerAge = null)
        {

            var result = _context.Games.AsNoTracking().AsQueryable();
            
            if(!string.IsNullOrWhiteSpace(nameFilter))
            {
                var normalizedQuery = nameFilter.Trim().ToLower();
                result = result.Where(game => game.Name.ToLower().Contains(normalizedQuery) || game.Aliases != null && game.Aliases.Any(alias => alias.Name.ToLower().Contains(normalizedQuery)));
            }
            
            var selectedCategoryIds = categoryIds?.Distinct().ToArray() ?? Array.Empty<int>();
            var selectedMechanicIds = mechanicIds?.Distinct().ToArray() ?? Array.Empty<int>();

            if (selectedCategoryIds.Length > 0 || selectedMechanicIds.Length > 0)
            {
                result = result.Where(game => game.Categories.Any(category => selectedCategoryIds.Contains(category.Id)) || game.Mechanics.Any(mechanic => selectedMechanicIds.Contains(mechanic.Id)));
            }

            if (publisherIds != null && publisherIds.Any())
            {
                result = result.Where(game =>
                    publisherIds.Contains(game.PublisherId));
            }

            if (designerIds != null && designerIds.Any())
            {
                result = result.Where(game => game.Designers != null && game.Designers.Any(designer => designerIds.Contains(designer.Id)));
            }

            if (minPlayers != null || maxPlayers != null)
            {
                if (minPlayers != null)
                {
                    result = result.Where(game => game.MinPlayers >= minPlayers);
                }
                if (maxPlayers != null)
                {
                    result = result.Where(game => game.MaxPlayers <= maxPlayers);
                }
            }
            else if(numberPlayers != null)
            {
                result = result.Where(game => game.MinPlayers <= numberPlayers && game.MaxPlayers >= numberPlayers);
            }

            if(youngestPlayerAge.HasValue)
            {
                result = result.Where(game => game.MinAge > 0 && game.MinAge <= youngestPlayerAge);
            }


            result = result.OrderByDescending(game => game.Categories.Count(category => selectedCategoryIds.Contains(category.Id)) + game.Mechanics.Count(mechanic => selectedMechanicIds.Contains(mechanic.Id))).ThenBy(game => game.Name).ThenBy(game => game.Id);
            return await result.ToListAsync();
        }
    }
}

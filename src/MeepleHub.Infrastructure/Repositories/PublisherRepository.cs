using Microsoft.EntityFrameworkCore;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;

namespace MeepleHub.Infrastructure.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly MeepleHubDbContext _dbContext;

        public PublisherRepository(MeepleHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Publisher?> GetByNameAsync(string name)
        {
            return await _dbContext.Publishers.FirstOrDefaultAsync(publisher => publisher.Name == name);
        }

        public async Task<Publisher> GetOrCreateAsync(string name)
        {
            var publisher = await GetByNameAsync(name);
            if (publisher is not null)
            {
                return publisher;
            }

            var newPublisher = new Publisher { Name = name };

            await _dbContext.Publishers.AddAsync(newPublisher);
            return newPublisher;
        }
    }   
}

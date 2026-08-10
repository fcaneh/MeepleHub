using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Domain.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher?> GetByNameAsync(string name);
        Task<Publisher> GetOrCreateAsync(string name);
    }
}

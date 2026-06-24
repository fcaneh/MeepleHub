using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.ExternalInterfaces;

namespace MeepleHub.Infrastructure.ExternalRepositories
{
    public class BggClient : IBggClient
    {
        private readonly HttpClient _httpClient;

        public BggClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetThingXmlByIdAsync(int bggId)
        {
            var url = $"https://boardgamegeek.com/xmlapi2/thing?id={bggId}&stats=1";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}

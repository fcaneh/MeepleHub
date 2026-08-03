using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Application.ExternalModels.Bgg;

namespace MeepleHub.Infrastructure.ExternalRepositories
{
    public class BggClient : IBggClient
    {
        private readonly HttpClient _httpClient;

        public BggClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BggClientResult> GetThingXmlByIdAsync(int bggId)
        {
            var url = $"https://boardgamegeek.com/xmlapi2/thing?id={bggId}&stats=1";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new BggClientResult
                {
                    Success = false,
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = $"Failed to fetch data from BGG API. Status code: {response.StatusCode}"
                };
            }

            return new BggClientResult
            {
                Success = true,
                StatusCode = (int)response.StatusCode,
                Xml = await response.Content.ReadAsStringAsync()
            };
        }
    }
}

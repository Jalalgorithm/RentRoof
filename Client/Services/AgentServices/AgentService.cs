using RentHome.Shared.DTOs;
using System.Net.Http.Json;

namespace RentHome.Client.Services.AgentServices
{
    public class AgentService : IAgentService
    {
        private readonly HttpClient httpClient;

        public AgentService( HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<GetAgentDTO>> GetAgents()
        {
            var result = await httpClient.GetAsync($"api/Agent");
            var response = await result.Content.ReadFromJsonAsync<List<GetAgentDTO>>();

            return response!;
        }
    }
}

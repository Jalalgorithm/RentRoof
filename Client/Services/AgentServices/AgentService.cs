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
        public async Task<List<HouseResponseDTO>> GetAgentProperty()
        {
            var result = await httpClient.GetAsync($"api/agent/AgentProp");
            var response = await result.Content.ReadFromJsonAsync<List<HouseResponseDTO>>();

            return response!;
        }
        

        public async Task<List<GetAgentDTO>> GetAgents()
        {
            var result = await httpClient.GetAsync($"api/Agent");
            var response = await result.Content.ReadFromJsonAsync<List<GetAgentDTO>>();

            return response!;
        }

        public async Task<List<AppointmentAwaitingDTO>> GetAppointmentAwaiting()
        {
            var result = await httpClient.GetAsync($"api/agent/Awaitingappointment");
            var response = await result.Content.ReadFromJsonAsync<List<AppointmentAwaitingDTO>>();

            return response!;
        }
    }
}

using RentHome.Shared.DTOs;
using System.Net.Http.Json;

namespace RentHome.Client.Services.ModeServices
{
    public class ModeService : IModeService
    {
        private readonly HttpClient httpClient;

        public ModeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<ModeResponseDTO>> GetAllMode()
        {
            var result = await httpClient.GetAsync("api/mode");
            var response = await result.Content.ReadFromJsonAsync<List<ModeResponseDTO>>();

            return response!;
        }
    }
}

using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Net.Http.Json;

namespace RentHome.Client.Services.HouseServices
{
    public class HouseService : IHouseService
    {
        private readonly HttpClient httpClient;

        public HouseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response> AddHouseData(HouseRequestDTO houseRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/house", houseRequest);
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<Response> DeleteHouseData(int id)
        {
            var result = await httpClient.DeleteAsync($"api/house/{id}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<List<HouseResponseDTO>> GetAllHouseData()
        {
            var result = await httpClient.GetAsync("api/house");
            var response = await result.Content.ReadFromJsonAsync<List<HouseResponseDTO>>();

            return response!;
        }

        public async Task<HouseResponseDTO> GetHouseData(int id)
        {
            var result = await httpClient.GetAsync($"api/house/{id}");
            var response = await result.Content.ReadFromJsonAsync<HouseResponseDTO>();

            return response!;
        }

        public async Task<Response> UpdateHouseData(HouseRequestDTO houseRequest, int id)
        {
            var result = await httpClient.PutAsJsonAsync($"api/house/{id}", houseRequest);
            var response = await result.Content.ReadFromJsonAsync<Response>();  

            return response!;
        }
    }
}

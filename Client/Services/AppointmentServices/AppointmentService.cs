using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Net.Http.Json;

namespace RentHome.Client.Services.AppointmentServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response> BookAppointment(int id)
        {
            var result = await httpClient.PostAsJsonAsync($"api/appointment/book/{id}" , new {});
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }
    }
}

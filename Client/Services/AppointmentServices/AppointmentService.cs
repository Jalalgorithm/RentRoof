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

        public async Task<Response> AlreadyBooked(int propertyId)
        {
            var result = await httpClient.GetAsync($"api/appointment/GetBookingStatus/{propertyId}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;

        }

        public async Task<Response> BookAppointment(int id)
        {
            var result = await httpClient.PostAsJsonAsync($"api/appointment/book/{id}" , new {});
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<Response> ConfirmAppointment(int id)
        {
            var result = await httpClient.PutAsJsonAsync($"api/appointment/ConfirmBooking/{id}" , new{});
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<Response> DeleteAppointment(int id)
        {
            var result = await httpClient.DeleteAsync($"api/appointment/FufillBooking/{id}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<List<GetBookingDTO>> GetBookingsForAgent()
        {
            var result = await httpClient.GetAsync("api/appointment/Getbooks");
            var response = await result.Content.ReadFromJsonAsync<List<GetBookingDTO>>();   

            return response!;
        }
    }
}

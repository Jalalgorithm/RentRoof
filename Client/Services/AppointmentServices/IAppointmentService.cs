using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<Response> BookAppointment(int id);
        Task<List<GetBookingDTO>> GetBookingsForAgent();
        Task<Response> ConfirmAppointment(int id);
        Task<Response> DeleteAppointment(int id);
        Task<Response> AlreadyBooked(int propertyId);
    }
}

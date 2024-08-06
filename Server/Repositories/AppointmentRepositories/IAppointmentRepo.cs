using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AppointmentRepositories
{
    public interface IAppointmentRepo
    {
        Task<Response> BookAppointment(int userId ,int id);
        Task<List<GetBookingDTO>> GetBookingsForAgent(int agentId);
        Task<Response> ConfirmAppointment(int id);
        Task<Response> DeleteAppointment(int id);
        Task<Response> AlreadyBooked(int userId , int propertyId);
    }
}

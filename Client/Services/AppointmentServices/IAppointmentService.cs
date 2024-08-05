using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<Response> BookAppointment(int id);
    }
}

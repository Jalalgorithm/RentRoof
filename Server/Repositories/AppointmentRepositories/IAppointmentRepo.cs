using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AppointmentRepositories
{
    public interface IAppointmentRepo
    {
        Task<Response> BookAppointment(int userId ,int id);
    }
}

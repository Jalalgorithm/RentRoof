using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.ModeServices
{
    public interface IModeService
    {
        Task<List<ModeResponseDTO>> GetAllMode();
    }
}

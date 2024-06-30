using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.ModeRepositories
{
    public interface IModeRepo
    {
        Task<List<ModeResponseDTO>> GetModeResposeAsync();
    }
}

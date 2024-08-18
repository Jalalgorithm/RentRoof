using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AgentRepositories
{
    public interface IAgentRepo
    {
        Task<List<GetAgentDTO>> GetAgent();
        Task<List<HouseResponseDTO>> GetAgentProperties(int agentId);
        Task<List<AppointmentAwaitingDTO>> GetUserAwaiting(int agentId);
    }
}

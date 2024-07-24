using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AgentRepositories
{
    public interface IAgentRepo
    {
        Task<List<GetAgentDTO>> GetAgent();
    }
}

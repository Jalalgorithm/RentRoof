using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.AgentServices
{
    public interface IAgentService
    {
        Task<List<GetAgentDTO>> GetAgents();
    }
}

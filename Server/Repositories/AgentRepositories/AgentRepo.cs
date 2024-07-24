using Microsoft.EntityFrameworkCore;
using RentHome.Server.Data;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AgentRepositories
{
    public class AgentRepo : IAgentRepo
    {
        private readonly ApplicationDbContext applicationDb;

        public AgentRepo(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public async Task<List<GetAgentDTO>> GetAgent()
        {
            var allAgent = await applicationDb.Agents
                .Select(agents => new GetAgentDTO
                {
                    Id = agents.Id,
                    FirstName = agents.FirstName,
                    LastName = agents.LastName,
                })
                .ToListAsync();

            return allAgent!;
        }
            
    }
}

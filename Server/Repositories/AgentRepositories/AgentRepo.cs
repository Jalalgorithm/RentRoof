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

        public async Task<List<HouseResponseDTO>> GetAgentProperties(int agentId)
        {
            var agentProp = await applicationDb.Houses
                .Where(x => x.AgentId == agentId)
                .Select(houses => new HouseResponseDTO
                {
                    Id = houses.Id,
                    Name = houses.Name,
                    Location = houses.Location,
                    Price = houses.Price,
                }).ToListAsync();

            return agentProp!;
        }

        public async Task<List<AppointmentAwaitingDTO>> GetUserAwaiting(int agentId)
        {

            var appointmentAwait = await applicationDb.Appointments
                .Include(h=>h.House)
                .Include(u=>u.User)
                .Where(x => x.AgentId == agentId && x.IsConfirmed == false)
                .Select(appoint => new AppointmentAwaitingDTO
                {
                    Id = appoint.Id,
                    FirstName = appoint.User.FirstName,
                    LastName = appoint.User.LastName,
                    AppointmentDate = appoint.AppointmentDate,
                    PropertyName = appoint.House.Name,
                    PropertyLocation = appoint.House.Location
                }).ToListAsync();

            return appointmentAwait!;
        }
    }
}

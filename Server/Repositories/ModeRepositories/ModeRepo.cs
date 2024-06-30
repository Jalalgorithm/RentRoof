using Microsoft.EntityFrameworkCore;
using RentHome.Server.Data;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.ModeRepositories
{
    public class ModeRepo : IModeRepo
    {
        private readonly ApplicationDbContext applicationDb;

        public ModeRepo(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public async Task<List<ModeResponseDTO>> GetModeResposeAsync()
        {
            var result = await applicationDb.Modes
                .Select(manyModes=> new ModeResponseDTO
                {
                    Id = manyModes.Id,
                    Name=manyModes.Name
                })
                .ToListAsync();

            return result;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Repositories.ModeRepositories;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeController : ControllerBase
    {
        private readonly IModeRepo modeRepo;

        public ModeController(IModeRepo modeRepo)
        {
            this.modeRepo = modeRepo;
        }

        public async Task<ActionResult<List<ModeResponseDTO>>> GetAllModes()
        {
            var result = await modeRepo.GetModeResposeAsync();

            if(result == null|| result.Count<1)
            {
                return BadRequest("Modes not found");
            }

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Repositories.AgentRepositories;
using RentHome.Shared.CustomResponse;

namespace RentHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepo agentRepo;

        public AgentController(IAgentRepo agentRepo)
        {
            this.agentRepo = agentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetAgent()
        {
            var agent = await agentRepo.GetAgent();

            if(agent == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No agent found"
                };

            }

            return Ok(agent);

        }
    }
}

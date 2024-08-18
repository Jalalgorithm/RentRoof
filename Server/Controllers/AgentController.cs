using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Data;
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

        [HttpGet("AgentProp")]
        public async Task<ActionResult<Response>> GetAgentProperties()
        {
            var agentId = JwtReader.GetUserId(User);

            if(agentId <1)
            {
                return new Response
                {
                    Success = false,
                    Message = "Agent not found"
                };
            }

            var agentProp = await agentRepo.GetAgentProperties(agentId);

            if(agentProp == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Couldnt fetch agent propertt details"
                };
            }

            return Ok(agentProp);
        }

        [HttpGet("Awaitingappointment")]
        public async Task<ActionResult<Response>> GetAppointmentAwaiters()
        {
            var agentId = JwtReader.GetUserId(User);

            if( agentId < 1)
            {
                return new Response
                {
                    Success = false,
                    Message = "No agent is found"
                };
            }

            var awaiters = await agentRepo.GetUserAwaiting(agentId);

            if(awaiters is null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Cant fetch wanted data"
                };
            }

            return Ok(awaiters);
        }
    }
}

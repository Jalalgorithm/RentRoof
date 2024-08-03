using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Data;
using RentHome.Server.Repositories.AccountRepositories;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Security.Claims;

namespace RentHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Response>> Login(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                return BadRequest("Input valid details");
            }

            var result = await accountRepo.Login(userLoginDTO);

            if(!result.Success)
            {
                return BadRequest("Couldnt complete log in operation");
            }
            return Ok(result);
        }

        [HttpPost("Login/Agent")]
        public async Task<ActionResult<Response>> LoginAgent(AgentLoginDTO agentLogin)
        {
            if (agentLogin == null)
            {
                return BadRequest("Input valid details");
            }

            var result = await accountRepo.LoginAgent(agentLogin);

            if(!result.Success)
            {
                return BadRequest("Couldnt complete log in operation");
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Response>> Register(UserRegisterDTO userRegister)
        {
            if (userRegister == null)
            {
                return BadRequest("Input valid details");
            }

            var result = await accountRepo.Register(userRegister);    

            if(!result.Success)
            {
                return NotFound("Couldnt complete sig in request");
            }

            return Ok(result);
        }

        [HttpPost("Register/Agent")]
        public async Task<ActionResult<Response>> RegisterAgent (RegisterAgentDTO registerAgent)
        {
            if(registerAgent == null)
            {
                return BadRequest("input valid details");
            }

            var result = await accountRepo.RegisterAgent(registerAgent);

            if(!result.Success)
            {
                return NotFound("Couldnt complete sign up request");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetProfile")]
        public async Task<ActionResult<UserResponseDTO>> GetProfile()
        {
            var email = CurrentUser();

            if(string.IsNullOrEmpty(email))
            {
                return BadRequest("User not found");
            }
            var result = await accountRepo.GetProfile(email);

            if(result==null)
            {
                return BadRequest("User does not exist");
            }

            return Ok(result);
        }
        [Authorize(Roles ="Agent")]
        [HttpGet("GetProfile/Agent")]
        public async Task<ActionResult<AgentProfileDTO>> GetAgentProfile()
        {
            var email = CurrentUser();

            if(string.IsNullOrEmpty(email))
            {
                return BadRequest("User not found");
            }
            var result = await accountRepo.GetAgentProfile(email);

            if (result==null)
            {
                return BadRequest("Agent not found");
            }

            return Ok(result);
        }

        private string CurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity==null)
            {
                return "";
            }

            var userClaims = identity.Claims!;

             var result = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value!;
            return result; 
        }
    }
}

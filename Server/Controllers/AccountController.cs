using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Data;
using RentHome.Server.Repositories.AccountRepositories;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

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

        [HttpGet("GetProfile")]
        public async Task<ActionResult<UserResponseDTO>> GetProfile()
        {
            var id = JwtReader.GetUserId(User);
            if(id==0)
            {
                return BadRequest("User not found");
            }
            var result = await accountRepo.GetProfile(id);

            if(result==null)
            {
                return BadRequest("user does not exist");
            }

            return Ok(result);
        }
    }
}

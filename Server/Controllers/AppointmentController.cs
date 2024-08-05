using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Data;
using RentHome.Server.Repositories.AppointmentRepositories;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Security.Claims;

namespace RentHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo appointmentRepo;

        public AppointmentController(IAppointmentRepo appointmentRepo )
        {
            this.appointmentRepo = appointmentRepo;
        }

        [Authorize (Roles ="Client")]
        [HttpPost("Book/{id}")]
        public async Task<ActionResult<Response>> BookAppointment(int id)
        {
            if (id <=0)
            {
                return BadRequest("No property found");
            }

            var userId = JwtReader.GetUserId(User);

            var result = await appointmentRepo.BookAppointment(userId, id);


            if(!result.Success)
            {
                return NotFound("Unable to complete");
            }
            return Ok(result);
        }
        private string CurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return "";
            }

            var userClaims = identity.Claims!;

            var result = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value!;
            return result;
        }

        private string CurrentUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return "";
            }

            var userClaims = identity.Claims!;

            var result = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value!;
            return result;
        }
    }
}

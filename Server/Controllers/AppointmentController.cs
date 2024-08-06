using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Data;
using RentHome.Server.Repositories.AppointmentRepositories;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Security.Claims;
using System.Text.RegularExpressions;

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

        [Authorize (Roles ="Agent")]
        [HttpGet("Getbooks")]
        public async Task<ActionResult<List<GetBookingDTO>>> GetAgentAppointmentList()
        {
            var AgentId = JwtReader.GetUserId(User);

            var result = await appointmentRepo.GetBookingsForAgent(AgentId);

            if(result==null)
            {
                return BadRequest("no bookings found");
            }

            return Ok(result);
        }

        [Authorize(Roles ="Agent")]
        [HttpPut("ConfirmBooking/{id}")]
        public async Task<ActionResult<Response>> ConfirmAppointment(int id)
        {
            if(id<=0)
            {
                return BadRequest("wrong or invalid id");
            }

            var result = await appointmentRepo.ConfirmAppointment(id);

            if(!result.Success)
            {
                return NotFound("couldnt confirm appointment");
            }

            return Ok(result);


        }

        [Authorize (Roles ="Agent")]
        [HttpDelete("FufillBooking/{id}")]
        public async Task<ActionResult<Response>> FulfilledBooking(int id)
        {
            if(id<=0)
            {
                return BadRequest("invalid id");
            }

            var result = await appointmentRepo.DeleteAppointment(id);

            if(!result.Success)
            {
                return NotFound("couldnt complete");
            }

            return Ok(result);
        }

        [HttpGet("GetBookingStatus/{propertyId}")]
        public async Task<ActionResult<Response>> GetStatus(int propertyId)
        {
            var userId = JwtReader.GetUserId(User);
            if(userId==0)
            {
                return BadRequest("Invalid userid");

            }
            if (propertyId<=0)
            {
                return BadRequest("Invalid propertyid");
            }

            var result = await appointmentRepo.AlreadyBooked(userId, propertyId);

            if(!result.Success)
            {
                return NotFound("couldnt complete");

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

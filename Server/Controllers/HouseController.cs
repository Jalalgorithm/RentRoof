using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentHome.Server.Repositories.HouseRepositories;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepo houseRepo;

        public HouseController(IHouseRepo houseRepo)
        {
            this.houseRepo = houseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<HouseResponseDTO>>> GetAllHouse()
        {
            var result = await houseRepo.GetHouseData();

            if (result == null)
            {
                return NotFound("No data found from the database");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseResponseDTO>> GetHouseById(int id)
        {
            var result = await houseRepo.GetHouseDataById(id);

            if(result==null)
            {
                return NotFound("no data found from the database");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteHouse(int id)
        {
            var result = await houseRepo.DeleteHouseData(id);

            if (!result.Success)
            {
                return BadRequest("cant delete selected property");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateHouse(HouseRequestDTO requestDTO , int id)
        {
            if (requestDTO == null)
            {
                return NoContent();
            }
            var result = await houseRepo.UpdateHouseData(requestDTO, id);

            if(!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddHouse ([FromForm]HouseRequestDTO requestDTO)
        {
            if(requestDTO == null)
            {
                return NoContent();
            }

            var result = await houseRepo.AddHouseData(requestDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPropList")]
        public async Task<ActionResult<Response>> GetPropertyTypeList()
        {
            var property = await houseRepo.GetPropertyType();

            if(property==null|| property.Count<1 )
            {
                return new Response
                {
                    Success = false,
                    Message = "No property Type Found"
                };


            }

            return Ok(property);
        }




    }
}

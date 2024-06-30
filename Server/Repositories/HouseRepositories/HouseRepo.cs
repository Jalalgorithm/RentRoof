using Microsoft.EntityFrameworkCore;
using RentHome.Server.Data;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using RentHome.Shared.Models;
using System.Net;

namespace RentHome.Server.Repositories.HouseRepositories
{
    public class HouseRepo : IHouseRepo
    {
        private readonly ApplicationDbContext _context;

        public HouseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> AddHouseData(HouseRequestDTO houseRequestDTO)
        {
            if (houseRequestDTO == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No content"
                };
            }

            if (await CheckName(houseRequestDTO.Name!))
            {
                return new Response
                {
                    Message = "Data Added already",
                    Success = false
                };
            }

            var house = new House()
            {
                Name = houseRequestDTO.Name,
                NumberOfBedroom = houseRequestDTO.NumberOfBedroom,
                NumberOfBathroom = houseRequestDTO.NumberOfBathroom,
                Price = houseRequestDTO.Price,
                ModeId = houseRequestDTO.ModeId,
                Type = houseRequestDTO.Type,
                Size = houseRequestDTO.Size,
                Location = houseRequestDTO.Location,
                Image = houseRequestDTO.Image

            };

            await _context.Houses.AddAsync(house);
            await _context.SaveChangesAsync();

            return new Response
            {
                Message = "Data has been successfully added",
                Success = true
            };
        }

        public async Task<Response> DeleteHouseData(int id)
        {
            var deleteHouse = await _context.Houses.FindAsync(id);

            if(deleteHouse == null)
            {
                return new Response
                {
                    Message = "House doesnt exist",
                    Success = false
                };
            }

            _context.Houses.Remove(deleteHouse);
            await _context.SaveChangesAsync();

            return new Response
            {
                Message = "Property has been deleted",
                Success = true
            };


        }

        public async Task<List<HouseResponseDTO>> GetHouseData()
        {
            var result = await _context.Houses
                .Include(m=>m.Mode)
                .Select(houseResponse=> new HouseResponseDTO
                {
                    Id= houseResponse.Id,
                    Name =houseResponse.Name,
                    Size =houseResponse.Size,
                    Price = houseResponse.Price,
                    NumberOfBathroom = houseResponse.NumberOfBathroom,
                    NumberOfBedroom = houseResponse.NumberOfBedroom,
                    Location = houseResponse.Location,
                    Mode = houseResponse.Mode.Name,
                    Image = houseResponse.Image,
                    DateCreated = houseResponse.DateCreated,
                    Type = houseResponse.Type,
                    ModeId = houseResponse.ModeId
                })
                .ToListAsync();

            return result;

           

        }

        public async Task<HouseResponseDTO> GetHouseDataById(int id)
        {
            var house = await _context.Houses
                .Include(m=>m.Mode)
                .Select(mainHouse=> new HouseResponseDTO
                {
                    Id=mainHouse.Id,
                    Name = mainHouse.Name,
                    Size = mainHouse.Size,
                    Price = mainHouse.Price,
                    NumberOfBathroom = mainHouse.NumberOfBathroom,
                    NumberOfBedroom = mainHouse.NumberOfBedroom,
                    Location = mainHouse.Location,
                    Mode = mainHouse.Mode.Name,
                    Type = mainHouse.Type,
                    Image = mainHouse.Image
                })
                .FirstOrDefaultAsync(h => h.Id == id);

            

            return house!;


        }

        public async Task<Response> UpdateHouseData(HouseRequestDTO houseRequestDTO, int id)
        {
            var mainHouse = await _context.Houses.FindAsync(id);

            if (mainHouse ==null)
            {
                return new Response
                {
                    Message = "No content",
                    Success = false
                };
            }

            if(await CheckName(houseRequestDTO.Name!))
            {
                return new Response
                {
                    Message = "Data already exist",
                    Success = false
                };
            }

            mainHouse.Name = houseRequestDTO.Name;
            mainHouse.Location = houseRequestDTO.Location;
            mainHouse.ModeId = houseRequestDTO.ModeId;
            mainHouse.NumberOfBathroom = houseRequestDTO.NumberOfBathroom;
            mainHouse.NumberOfBedroom = houseRequestDTO.NumberOfBedroom;
            mainHouse.Price = houseRequestDTO.Price;
            mainHouse.Type = houseRequestDTO.Type;
            mainHouse.Image = houseRequestDTO.Image;

            await _context.SaveChangesAsync();

            return new Response
            {
                Message = "Peoperty updated successfully",
                Success = true
            };
        }

        private async Task<bool> CheckName(string name)
        {
            var DoesExist = await _context.Houses.Where(h => h.Name.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();

            return DoesExist == null ? false : true;
        }
    }
}

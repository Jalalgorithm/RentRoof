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
        private readonly ImageHandler imageHandler;

        public HouseRepo(ApplicationDbContext context , ImageHandler imageHandler)
        {
            _context = context;
            this.imageHandler = imageHandler;
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

            if(houseRequestDTO==null ||houseRequestDTO.OtherImages.Count<1)
            {
                return new Response
                {
                    Success = false,
                    Message = "No picture found"
                };
            }
            var imageUrl = imageHandler.UploadImage(houseRequestDTO.Image);

            var listofImage = imageHandler.UploadManyImages(houseRequestDTO.OtherImages);
            
            if (string.IsNullOrEmpty(imageUrl))
            {
                return new Response
                {
                    Success = false,
                    Message = "issue uploading image"
                };
            }

            var propImage = new List<HouseImage>();

            

            foreach (var url in listofImage)
            {
                var imUrl = new HouseImage();
                imUrl.FilePath = url ;
                propImage.Add(imUrl);
            }

            var house = new House()
            {
                Name = houseRequestDTO.Name,
                Description = houseRequestDTO.Description,
                NumberOfBedroom = houseRequestDTO.NumberOfBedroom,
                NumberOfBathroom = houseRequestDTO.NumberOfBathroom,
                Price = houseRequestDTO.Price,
                ModeId = houseRequestDTO.ModeId,
                AgentId = houseRequestDTO.AgentId,
                TypeOfPropertyId = houseRequestDTO.TypeofPropertyId,
                Size = houseRequestDTO.Size,
                Location = houseRequestDTO.Location,
                Image = imageUrl,
                OtherImages = propImage.ToList()
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
            var deleteHouse = await _context.Houses
                .Include(h => h.OtherImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(deleteHouse == null)
            {
                return new Response
                {
                    Message = "House doesnt exist",
                    Success = false
                };
            }

            imageHandler.DeleteAnImage(deleteHouse.Image);
            imageHandler.DeleteManyImages(deleteHouse.OtherImages.Select(i=>i.FilePath).ToList());

            _context.HouseImages.RemoveRange(deleteHouse.OtherImages);
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
                .Include(p=>p.TypeOfProperty)
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
                    TypeofProperty = houseResponse.TypeOfProperty.Name,
                    Image = houseResponse.Image,
                    DateCreated = houseResponse.DateCreated,
                    ModeId = houseResponse.ModeId
                })
                .ToListAsync();

            return result;

           

        }

        public async Task<HouseResponseDetail> GetHouseDataById(int id)
        {
            var house = await _context.Houses
                .Include(m=>m.Mode)
                .Include(p=>p.TypeOfProperty)
                .Select(mainHouse=> new HouseResponseDetail
                {
                    Id=mainHouse.Id,
                    Name = mainHouse.Name,
                    Description = mainHouse.Description,
                    Size = mainHouse.Size,
                    Price = mainHouse.Price,
                    NumberOfBathroom = mainHouse.NumberOfBathroom,
                    NumberOfBedroom = mainHouse.NumberOfBedroom,
                    Location = mainHouse.Location,
                    Mode = mainHouse.Mode.Name,
                    TypeofProperty = mainHouse.TypeOfProperty.Name,
                    Image = mainHouse.Image,
                    OtherImages = mainHouse.OtherImages.Select(i=>i.FilePath).ToList()
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



            if (houseRequestDTO == null || houseRequestDTO.OtherImages.Count < 1)
            {
                return new Response
                {
                    Success = false,
                    Message = "No picture found"
                };
            }
            var imageUrl = imageHandler.UploadImage(houseRequestDTO.Image);

            var listofImage = imageHandler.UploadManyImages(houseRequestDTO.OtherImages);

            if (string.IsNullOrEmpty(imageUrl))
            {
                return new Response
                {
                    Success = false,
                    Message = "issue uploading image"
                };
            }

            var propImage = new List<HouseImage>();



            foreach (var url in listofImage)
            {
                var imUrl = new HouseImage();
                imUrl.FilePath = url;
                propImage.Add(imUrl);
            }

            mainHouse.Name = houseRequestDTO.Name;
            mainHouse.Description = houseRequestDTO.Description;
            mainHouse.Location = houseRequestDTO.Location;
            mainHouse.ModeId = houseRequestDTO.ModeId;
            mainHouse.AgentId = houseRequestDTO.AgentId;
            mainHouse.TypeOfPropertyId = houseRequestDTO.TypeofPropertyId;
            mainHouse.NumberOfBathroom = houseRequestDTO.NumberOfBathroom;
            mainHouse.NumberOfBedroom = houseRequestDTO.NumberOfBedroom;
            mainHouse.Price = houseRequestDTO.Price;
            mainHouse.Image = imageUrl;
            mainHouse.OtherImages = propImage.ToList();

            await _context.SaveChangesAsync();

            return new Response
            {
                Message = "Peoperty updated successfully",
                Success = true
            };
        }


        public async Task<List<GetPropertyTypeDTO>> GetPropertyType()
        {
            var properties = await _context.PropertyTypes
                .Select(property=> new GetPropertyTypeDTO
                {
                    Id= property.Id,
                    PropertyName = property.Name
                })
                .ToListAsync();


            return properties!;

        }

        private async Task<bool> CheckName(string name)
        {
            var DoesExist = await _context.Houses.Where(h => h.Name.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();

            return DoesExist == null ? false : true;
        }
    }
}

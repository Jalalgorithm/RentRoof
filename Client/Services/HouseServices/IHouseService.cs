using Microsoft.AspNetCore.Components.Forms;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.HouseServices
{
    public interface IHouseService
    {
        Task<Response> AddHouseData(HouseRequestDTO houseRequest , IBrowserFile imageFile, List<IBrowserFile> otherImageFiles);
        Task<Response> UpdateHouseData(HouseRequestDTO houseRequest , int id);
        Task<Response> DeleteHouseData(int id);
        Task<HouseResponseDetail> GetHouseData(int id);
        Task<List<HouseResponseDTO>> GetAllHouseData();
        Task<List<GetPropertyTypeDTO>> GetPropertyTypeList();
    }
}

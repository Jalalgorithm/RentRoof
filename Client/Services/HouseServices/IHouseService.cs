using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.HouseServices
{
    public interface IHouseService
    {
        Task<Response> AddHouseData(HouseRequestDTO houseRequest);
        Task<Response> UpdateHouseData(HouseRequestDTO houseRequest , int id);
        Task<Response> DeleteHouseData(int id);
        Task<HouseResponseDTO> GetHouseData(int id);
        Task<List<HouseResponseDTO>> GetAllHouseData();
    }
}

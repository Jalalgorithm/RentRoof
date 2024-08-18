using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using RentHome.Shared.Models;

namespace RentHome.Server.Repositories.HouseRepositories
{
    public interface IHouseRepo
    {
        Task<Response> AddHouseData(HouseRequestDTO houseRequestDTO , int agentId);
        Task<Response> UpdateHouseData(HouseRequestDTO houseRequestDTO , int id);
        Task<Response> DeleteHouseData(int id);
        Task<List<HouseResponseDTO>> GetHouseData();
        Task<HouseResponseDetail> GetHouseDataById(int id);
        Task<List<GetPropertyTypeDTO>> GetPropertyType();
        Task<List<HouseResponseDTO>> SearchHouse(SearchDTO searchDTO);
        Task<List<HouseResponseDTO>> GetHousesByPropertyType(int id);

    }
}

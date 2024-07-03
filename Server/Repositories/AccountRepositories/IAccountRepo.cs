using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AccountRepositories
{
    public interface IAccountRepo
    {
        Task<Response> Register(UserRegisterDTO userRegister);
        Task<Response> Login(UserLoginDTO userLogin);
        Task<UserResponseDTO> GetProfile(string email);
    }
}

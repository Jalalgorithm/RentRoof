using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.AccountServices
{
    public interface IAccountService
    {
        Task<Response> Login(UserLoginDTO userLogin);
        Task<Response> Register(UserRegisterDTO userRegister);
        Task<UserResponseDTO> GetProfile();
    }
}

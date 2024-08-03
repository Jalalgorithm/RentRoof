using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Client.Services.AccountServices
{
    public interface IAccountService
    {
        Task<Response> Login(UserLoginDTO userLogin);
        Task<Response> Register(UserRegisterDTO userRegister);
        Task<UserResponseDTO> GetProfile();
        Task<AgentProfileDTO> GetAgentProfile();
        Task<Response> LoginAgent(AgentLoginDTO agentLogin);
        Task<Response> RegisterAgent(RegisterAgentDTO agentRegister);
    }
}

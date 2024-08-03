using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AccountRepositories
{
    public interface IAccountRepo
    {
        Task<Response> Register(UserRegisterDTO userRegister);
        Task<Response> Login(UserLoginDTO userLogin);
        Task<UserResponseDTO> GetProfile(string email);
        Task<Response> RegisterAgent(RegisterAgentDTO agent);
        Task<Response> LoginAgent(AgentLoginDTO agentLogin);
        Task<AgentProfileDTO> GetAgentProfile(string email);
    }
}

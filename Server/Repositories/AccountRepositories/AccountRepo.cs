using RentHome.Server.Data;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;

namespace RentHome.Server.Repositories.AccountRepositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public AccountRepo( ApplicationDbContext dbContext ,IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        public Task<Response> GetProfile()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Login(UserLoginDTO userLogin)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Register(UserRegisterDTO userRegister)
        {
            throw new NotImplementedException();
        }
    }
}

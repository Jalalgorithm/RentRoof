using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentHome.Server.Data;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using RentHome.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<UserResponseDTO> GetProfile(string email)
        {
            var user = await dbContext.Users
                .Select(authUser => new UserResponseDTO
                {
                    Id = authUser.Id,
                    FirstName = authUser.FirstName,
                    LastName = authUser.LastName,
                    Email = authUser.Email,
                    Phone = authUser.Phone,
                    Address = authUser.Address
                })
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            return user;    
        }

        public async Task<Response> Login(UserLoginDTO userLogin)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == userLogin.Email);

            if (user is null)
            {
                return new Response
                {
                    Message = "Email or password is incorrect",
                    Success = false

                };
            }

            var passwordHasher = new PasswordHasher<User>();
            var getPassword = passwordHasher.VerifyHashedPassword(new User(), user.Password, userLogin.Password);

            if(getPassword ==PasswordVerificationResult.Failed)
            {
                return new Response
                {
                    Message = "Email or password is incorrect",
                    Success = false
                };
            }

            var jwt = CreateJwt(user);

            return new Response
            {
                Message = jwt,
                Success = true
            };
        }

        public async Task<Response> LoginAgent(AgentLoginDTO agentLogin)
        {
            var agent = await dbContext.Agents.FirstOrDefaultAsync(x => x.Email == agentLogin.Email);

            if (agent is null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                };
            }

            var passwordHasher = new PasswordHasher<Agent>();
            var getPassword = passwordHasher.VerifyHashedPassword(new Agent(), agent.Password, agentLogin.Password);

            if(getPassword ==PasswordVerificationResult.Failed)
            {
                return new Response
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                };
            }

            var jwt = CreateAgentJwt(agent);

            return new Response
            {
                Success = true,
                Message = jwt
            };
        }

        public async Task<Response> Register(UserRegisterDTO userRegister)
        {
            var emailVerify = await dbContext.Users.CountAsync(e => e.Email == userRegister.Email);
            if (emailVerify >0)
            {
                return new Response
                {
                    Message = "Email or password already exist",
                    Success = false

                };
            }

            var passwordHasher = new PasswordHasher<User>();

            var encryptedPassword = passwordHasher.HashPassword(new User(), userRegister.Password);

            User user = new()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Address = userRegister.Address,
                Phone = userRegister.Phone,
                Password= encryptedPassword,
                Role = "Client",
                CreatedAt = DateTime.Now
            };


            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "Account has been created succesfully",
                Success = true
            };

        }

        public async Task<Response> RegisterAgent(RegisterAgentDTO regAgent)
        {
            var verifyMail = await dbContext.Agents.CountAsync(x => x.Email == regAgent.Email);

            if(verifyMail >0)
            {
                return new Response
                {
                    Success = false,
                    Message = "Email already exist"
                };
            }

            if(regAgent == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Input valid details"
                };

            }

            var passwordHasher = new PasswordHasher<Agent>();

            var encryptedPassword = passwordHasher.HashPassword(new Agent(), regAgent.Password);

            var agent = new Agent()
            {
                FirstName = regAgent.FirstName,
                LastName = regAgent.LastName,
                Password = encryptedPassword,
                Phone = regAgent.Phone,
                Email = regAgent.Email,
                Address = regAgent.Address,
                Role = "Agent",
                AgentStatusId = 1
            };

            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();

            return new Response
            {
                Success = true,
                Message = "Agent added successfully"
            };
        }

        private string CreateJwt (User user)
        {
            List<Claim> claims = new()
            {
                new Claim("id" , "" +user.Id),
                new Claim(ClaimTypes.Role , user.Role),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Name , user.FirstName)
            };

            string strKey = configuration["JwtSettings:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private string CreateAgentJwt(Agent agent)
        {
            List<Claim> claims = new()
            {
                new Claim("id" , "" +agent.Id),
                new Claim(ClaimTypes.Role , agent.Role),
                new Claim(ClaimTypes.Email , agent.Email),
                new Claim(ClaimTypes.Name , agent.FirstName)
            };

            string strKey = configuration["JwtSettings:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        public async Task<AgentProfileDTO> GetAgentProfile(string email)
        {
            var thisAgent = await dbContext.Agents
                .Select(agent=> new AgentProfileDTO
                {
                    Id = agent.Id,
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    Email = agent.Email,
                    PhoneNumber = agent.Phone,
                    Address = agent.Address
                }).FirstOrDefaultAsync(x=>x.Email.ToLower()==email.ToLower());


            return thisAgent!;
        }
    }
}

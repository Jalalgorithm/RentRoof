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
        public async Task<UserResponseDTO> GetProfile(int id)
        {
            var user = await dbContext.Users
                .Select(authUser => new UserResponseDTO
                {
                    Id = authUser.Id,
                    FirstName = authUser.FirstName,
                    LastName = authUser.LastName,
                    Email = authUser.Email,
                    Phone = authUser.Phone
                })
                .FirstOrDefaultAsync(u => u.Id == id);

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


        private string CreateJwt (User user)
        {
            List<Claim> claims = new()
            {
                new Claim("id" , "" +user.Id),
                new Claim("role" , user.Role)
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
    }
}

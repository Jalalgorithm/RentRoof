using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RentHome.Client.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<UserResponseDTO> GetProfile()
        {
            var result = await httpClient.GetAsync("api/account/GetProfile");
            var response  = await result.Content.ReadFromJsonAsync<UserResponseDTO>();

            return response!;
        }


        public async Task<Response> Login(UserLoginDTO userLogin)
        {
            var result = await httpClient.PostAsJsonAsync("api/account/Login" , userLogin);
            var response = await result.Content.ReadFromJsonAsync <Response>();

            return response!;
        }

        public async Task<Response> Register(UserRegisterDTO userRegister)
        {
            var result = await httpClient.PostAsJsonAsync("api/account/Register", userRegister);
            var response = await result.Content.ReadFromJsonAsync<Response>(); 
            
            return response!;
        }
    }
}

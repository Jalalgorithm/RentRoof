using Microsoft.AspNetCore.Components.Forms;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using System.Net.Http.Json;

namespace RentHome.Client.Services.HouseServices
{
    public class HouseService : IHouseService
    {
        private readonly HttpClient httpClient;

        public HouseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response> AddHouseData(HouseRequestDTO houseRequest, IBrowserFile imageFile, List<IBrowserFile> otherImageFiles)
        {
            var content = new MultipartFormDataContent();

           
            if (imageFile != null)
            {
                var imageContent = new StreamContent(imageFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024));
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
                content.Add(imageContent, "Image", imageFile.Name);
            }

            
            if (otherImageFiles != null && otherImageFiles.Count > 0)
            {
                foreach (var file in otherImageFiles)
                {
                    var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024));
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, "OtherImages", file.Name);
                }
            }

            // Add other form data fields
            content.Add(new StringContent(houseRequest.Name ?? ""), "Name");
            content.Add(new StringContent(houseRequest.Description), "Description");
            content.Add(new StringContent(houseRequest.ModeId.ToString()), "ModeId");
            content.Add(new StringContent(houseRequest.TypeofPropertyId.ToString()), "TypeofPropertyId");
            content.Add(new StringContent(houseRequest.AgentId.ToString()), "AgentId");
            content.Add(new StringContent(houseRequest.Location ?? ""), "Location");
            content.Add(new StringContent(houseRequest.Price.ToString()), "Price");
            content.Add(new StringContent(houseRequest.Size.ToString()), "Size");
            content.Add(new StringContent(houseRequest.NumberOfBedroom.ToString()), "NumberOfBedroom");
            content.Add(new StringContent(houseRequest.NumberOfBathroom.ToString()), "NumberOfBathroom");

            var result = await httpClient.PostAsync("api/house", content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Response>();
                return response!;
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Cant complete service"
                };
            }
        }

        public async Task<Response> DeleteHouseData(int id)
        {
            var result = await httpClient.DeleteAsync($"api/house/{id}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            return response!;
        }

        public async Task<List<HouseResponseDTO>> GetAllHouseData()
        {
            var result = await httpClient.GetAsync("api/house");
            var response = await result.Content.ReadFromJsonAsync<List<HouseResponseDTO>>();

            return response!;
        }

        public async Task<HouseResponseDetail> GetHouseData(int id)
        {
            var result = await httpClient.GetAsync($"api/house/{id}");
            var response = await result.Content.ReadFromJsonAsync<HouseResponseDetail>();

            return response!;
        }

        public async Task<Response> UpdateHouseData(HouseRequestDTO houseRequest, int id)
        {
            var result = await httpClient.PutAsJsonAsync($"api/house/{id}", houseRequest);
            var response = await result.Content.ReadFromJsonAsync<Response>();  

            return response!;
        }

        public async Task<List<GetPropertyTypeDTO>> GetPropertyTypeList()
        {
            var result = await httpClient.GetAsync($"api/house/GetPropList");
            var response = await result.Content.ReadFromJsonAsync<List<GetPropertyTypeDTO>>();

            return response!;
        }
    }
}

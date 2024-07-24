using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.DTOs
{
    public class HouseRequestDTO
    {
        public string? Name { get; set; }
        public string Description { get; set; }
        public int ModeId { get; set; }
        public int TypeofPropertyId { get; set; }

        public int AgentId { get; set; }
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int NumberOfBedroom { get; set; }
        public int NumberOfBathroom { get; set; }
        public IFormFile Image { get; set; }
        public List<IFormFile> OtherImages { get; set; }
    }
}

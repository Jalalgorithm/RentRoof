using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.DTOs
{
    public class HouseResponseDetail
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public string? Mode { get; set; }
        public int ModeId { get; set; }
        public string TypeofProperty { get; set; }
        public int TypeofPropertyId { get; set; }
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int NumberOfBedroom { get; set; }
        public int NumberOfBathroom { get; set; }
        public string? Image { get; set; }
        public List<string> OtherImages { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}

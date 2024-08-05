using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.DTOs
{
    public class SearchDTO
    {
        public string? Keyword { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? Location { get; set; }
    }
}

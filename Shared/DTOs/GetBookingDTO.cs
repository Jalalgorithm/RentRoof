using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.DTOs
{
    public  class GetBookingDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime DateScheduled { get; set; }
        public string PropertyName { get; set; }
        public bool isConfirmed { get; set; }
    }
}

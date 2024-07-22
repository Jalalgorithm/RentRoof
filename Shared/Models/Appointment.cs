using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.Models
{
    public  class Appointment
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHome.Shared.DTOs
{
    public class AppointmentAwaitingDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string  PropertyName { get; set; }
        public string PropertyLocation { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}

using MailerSend.AspNetCore;
using Microsoft.EntityFrameworkCore;
using RentHome.Server.Data;
using RentHome.Shared.CustomResponse;
using RentHome.Shared.DTOs;
using RentHome.Shared.Models;

namespace RentHome.Server.Repositories.AppointmentRepositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response> AlreadyBooked(int userId, int propertyId)
        {
            var booked = await dbContext.Appointments
                .FirstOrDefaultAsync(x => x.UserId == userId && x.HouseId == propertyId);

            if (booked == null)
            {
                return new Response
                {
                    Success = true,
                    Message = "0"
                };
            }

            return new Response
            {
                Success = true,
                Message = "1"
            };


        }

        public async Task<Response> BookAppointment(int userId,int id)
        {
            if (userId == 0)
            {
                return new Response
                {
                    Success = false,
                    Message = "No user or agent found for this property"
                };
            }

            var property = await dbContext.Houses
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(x => x.Id == id );

            if (property == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Property is not definitve"
                };
            }

            

            var appointment = new Appointment()
            {
                HouseId = property.Id,
                UserId = userId,
                AgentId =property.AgentId, 
                AppointmentDate = GenerateRandomDate(),
                IsConfirmed = false,

            };

            var user = await dbContext.Users.FindAsync(userId);
            if(user is null)
            {
                return new Response
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            await dbContext.Appointments.AddAsync(appointment);
            await dbContext.SaveChangesAsync();


            return new Response
            {
                Success = true,
                Message = $" Dear {user.FirstName} , Your Appointment for {property.Name} , is slated for {appointment.AppointmentDate} , with our Agent {property.Agent.FirstName}. This is a notice of booking and doesnt confirm this meeting,  further communication will be sent to your mail , Thank you."
            };

           
        }

        public async Task<Response> ConfirmAppointment(int id)
        {
            var appointment = await dbContext.Appointments
                .Include(u=>u.User)
                .FirstOrDefaultAsync(x=>x.Id == id);

            if(appointment is null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Appointment not found"
                };
            }

            appointment.IsConfirmed = true;

            await dbContext.SaveChangesAsync();

            return new Response
            {
                Success = true,
                Message =$"Appointment now confirmed and booked to hold with {appointment.User.FirstName + "" + appointment.User.LastName } with details {appointment.User.Email} and {appointment.User.Phone} please do well to communicate and meet in an open space. Thank you"
            };
        }

        public async Task<Response> DeleteAppointment(int id)
        {
            var delAppointment = await dbContext.Appointments.FindAsync(id);

            if (delAppointment is null)
            {
                return new Response
                {
                    Success = false,
                    Message = "appointment not found"
                };
            }

            dbContext.Appointments.Remove(delAppointment);
            await dbContext.SaveChangesAsync();

            return new Response
            {
                Success = true,
                Message = "Appointment has been declined and communicated with the user"
            };
        }

        public async Task<List<GetBookingDTO>> GetBookingsForAgent(int agentId)
        {
            var bookings = await dbContext.Appointments
                .Include(h => h.House)
                .Include(u => u.User)
                .Where(x=>x.AgentId==agentId)
                .Select(appoint => new GetBookingDTO
                {
                    Id = appoint.Id,
                    PropertyName = appoint.House.Name,
                    isConfirmed = appoint.IsConfirmed,
                    User = appoint.User.FirstName
                }).ToListAsync();

            return bookings!;
        }

        private DateTime GenerateRandomDate()
        {
            Random random = new Random();
            DateTime start = DateTime.Now.AddDays(2);
            DateTime end = DateTime.Now.AddDays(10); 
            int range = (end - start).Days;

            DateTime randomDate =  start.AddDays(random.Next(range));

            return new DateTime(randomDate.Year, randomDate.Month , randomDate.Day , random.Next(10,15) ,random.Next(60) , 0);
        }
    }
}

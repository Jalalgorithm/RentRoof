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
                .FirstOrDefaultAsync(x => x.Id == id);

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
                HouseId = id,
                UserId = userId,
                AgentId =property.AgentId, 
                AppointmentDate = GenerateRandomDate(),
                IsConfirmed = false,

            };


            await dbContext.Appointments.AddAsync(appointment);
            await dbContext.SaveChangesAsync();


            return new Response
            {
                Success = true,
                Message = $"Appointment for {property.Name} , is slated for {appointment.AppointmentDate} , with our Agent {property.Agent.FirstName}. This is a notice of booking further communication will be sent to your mail , Thank you."
            };

           
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

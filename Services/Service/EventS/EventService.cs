using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Services.IService.IEvents;
using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Services.Service.EventS
{
    public class EventService : IEventService
    {
        private readonly ApplicationDBContext _context;
        public EventService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetEventsByUser(int idUser)
        {
            try
            {
                var eventsFound = await _context.Events.Where(e => e.IdOrganizer == idUser).ToListAsync();
                if (eventsFound == null)
                {
                    throw new Exception("Events not found");
                }
                return eventsFound;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> GetEventById(int idEvent)
        {
            try
            {
                var eventFound = await _context.Events.FindAsync(idEvent);
                if (eventFound == null)
                {
                    throw new Exception("Event not found");
                }
                return eventFound;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> CreateEvent(Event eventToCreate)
        {
            try
            {
                var organizer = await _context.Users.FindAsync(eventToCreate.IdOrganizer);
                if (organizer == null)
                {
                    throw new Exception("Organized not found");
                }
                TimeZoneInfo gmt5Zone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime eventDateTimeInGmt5 = TimeZoneInfo.ConvertTimeFromUtc(eventToCreate.EventDateTime, gmt5Zone);
                Event newEvent = new Event()
                {
                    Name = eventToCreate.Name,
                    Description = eventToCreate.Description,
                    Location = eventToCreate.Location,
                    EventDateTime = eventDateTimeInGmt5,
                    IdOrganizer = eventToCreate.IdOrganizer,
                    Organizer = organizer
                };
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();
                return newEvent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> UpdateEvent(Event eventToUpdate)
        {
            try
            {
                var eventFound = await _context.Events.FindAsync(eventToUpdate.Id);
                if (eventFound == null)
                {
                    throw new Exception("Event not found");
                }
                eventFound.Name = eventToUpdate.Name;
                eventFound.Description = eventToUpdate.Description;
                eventFound.Location = eventToUpdate.Location;
                eventFound.EventDateTime = eventToUpdate.EventDateTime;
                eventFound.IsActive = eventToUpdate.IsActive;
                await _context.SaveChangesAsync();
                return eventFound;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> DeleteEvent(int idEvent)
        {
            try
            {
                var eventFound = await _context.Events.FindAsync(idEvent);
                if (eventFound == null)
                {
                    throw new Exception("Event not found");
                }
                _context.Events.Remove(eventFound);
                await _context.SaveChangesAsync();
                return eventFound;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

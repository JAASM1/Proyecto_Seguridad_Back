using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
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

        public async Task<Event> GetEventByToken(string Token)
        {
            try
            {
                var eventFound = await _context.Events.Where(x => x.Token == Token).FirstOrDefaultAsync();
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

        public async Task<List<Event>> GetEventByInvitation(int idUser)
        {
            try
            {
                var eventsFound = await _context.Invitations
                    .Where(i => i.IdUser == idUser)
                    .Join(_context.Events, i => i.IdEvent, e => e.Id, (i, e) => e)
                    .ToListAsync();
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

        public async Task<List<Models.App.User>> GetInvitedUsersByEvent(int idEvent)
        {
            try
            {
                var invitedUsers = await _context.Invitations
                    .Where(i => i.IdEvent == idEvent)
                    .Join(_context.Users, i => i.IdUser, u => u.Id, (i, u) => u)
                    .ToListAsync();

                if (!invitedUsers.Any())
                {
                    throw new Exception("No invited users found for this event.");
                }

                return invitedUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> CreateInvitationEvent(InvitationDTO invitationdto)
        {
            try
            {
                if (invitationdto.IdEvent== 0)
                    return false;

                Event @event = await _context.Events.SingleOrDefaultAsync(x => x.Id == invitationdto.IdEvent);
                if (@event == null)
                    return false;

                if(@event.IdOrganizer ==invitationdto.IdUser)
                    throw new Exception("No puedes invitarte a tu propio evento 😒");

                if (invitationdto.IdUser <= 0)
                    return false;

                Models.App.User user = await _context.Users.SingleOrDefaultAsync(x => x.Id == invitationdto.IdUser);
                if (user == null)
                    return false;
                
                await _context.Invitations.AddAsync(new Invitation
                {
                    IdUser = invitationdto.IdUser,
                    IdEvent = invitationdto.IdEvent,
                    InvitedAt = DateTime.Now
                });

                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la invitación.", ex);
            }
        }

        public async Task<bool> CreateEvent(Event eventToCreate)
        {
            try
            {
                var organizer = await _context.Users.FindAsync(eventToCreate.IdOrganizer) ?? throw new Exception("Organized not found");
                
                Event newEvent = new()
                {
                    Name = eventToCreate.Name,
                    Description = eventToCreate.Description,
                    Location = eventToCreate.Location,
                    EventDateTime = eventToCreate.EventDateTime,
                    IdOrganizer = eventToCreate.IdOrganizer,
                };

                await _context.Events.AddAsync(newEvent);
                var result = await _context.SaveChangesAsync();
                return result > 0;
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

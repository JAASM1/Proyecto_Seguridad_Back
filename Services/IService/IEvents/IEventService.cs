using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;

namespace back_sistema_de_eventos.Services.IService.IEvents
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsByUser(int idUser);
        Task<Event> GetEventById(int idEvent);
        Task<Event> GetEventByToken(string Token);
        Task<List<Event>> GetEventByInvitation(int idUser);
        Task<bool> CreateEvent(Event eventToCreate);
        Task<Event> UpdateEvent(Event eventToUpdate);
        Task<Event> DeleteEvent(int idEvent);
        Task<List<User>> GetInvitedUsersByEvent(int idEvent);
        Task<bool> CreateInvitationEvent(InvitationDTO invitationdto);
    }
}

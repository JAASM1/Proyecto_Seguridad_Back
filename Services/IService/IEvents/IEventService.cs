using back_sistema_de_eventos.Models.App;

namespace back_sistema_de_eventos.Services.IService.IEvents
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsByUser(int idUser);
        Task<Event> GetEventById(int idEvent);
        Task<Event> CreateEvent(Event eventToCreate);
        Task<Event> UpdateEvent(Event eventToUpdate);
        Task<Event> DeleteEvent(int idEvent);

    }
}

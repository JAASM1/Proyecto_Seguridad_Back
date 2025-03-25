using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.IService.IEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_sistema_de_eventos.Controllers.EventControllers
{
    [Route("api/[controller]")]  // Define la ruta base
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: EventController/GetEventByUser/5
        [Authorize]
        [HttpGet("GetEventsByUser/{idUser}")]
        public async Task<ActionResult> GetEventByUser(int idUser)
        {
            var result = await _eventService.GetEventsByUser(idUser);
            return Ok(result);
        }

        // GET: EventController/GetEventById/5
        [Authorize]
        [HttpGet("GetEventById/{idEvent}")]
        public async Task<ActionResult> GetEventById(int idEvent)
        {
            var result = await _eventService.GetEventById(idEvent);
            return Ok(result);
        }

        // GET: EventController/GetEventById/44f45hb7-g798-4de9-ccc7-75ca1a150342
        [HttpGet("GetEventByToken/{Token}")]
        public async Task<ActionResult> GetEventByToken(string Token)
        {
            var result = await _eventService.GetEventByToken(Token);
            return Ok(result);
        }

        // POST: EventController/Create
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] EventDTO eventDTO)
        {
            var eventToCreate = new Event
            {
                Name = eventDTO.Name,
                Description = eventDTO.Description,
                Location = eventDTO.Location,
                EventDateTime = eventDTO.EventDateTime,
                IsActive = eventDTO.IsActive,
                IdOrganizer = eventDTO.IdOrganizer
            };
            var result = await _eventService.CreateEvent(eventToCreate);
            return Ok(result);
        }

        // PUT: EventController/Update/5
        [HttpPut("Update/{idEvent}")]
        public async Task<ActionResult> Update(int idEvent, [FromBody] EventDTO eventDTO)
        {
            var eventToUpdate = new Event
            {
                Id = idEvent,
                Name = eventDTO.Name,
                Description = eventDTO.Description,
                Location = eventDTO.Location,
                EventDateTime = eventDTO.EventDateTime,
                IsActive = eventDTO.IsActive,
                IdOrganizer = eventDTO.IdOrganizer
            };
            var result = await _eventService.UpdateEvent(eventToUpdate);
            return Ok(result);
        }

        // DELETE: EventController/Delete/5
        [HttpDelete("Delete/{idEvent}")]
        public async Task<ActionResult> Delete(int idEvent)
        {
            var result = await _eventService.DeleteEvent(idEvent);
            return Ok(result);
        }
    }

}

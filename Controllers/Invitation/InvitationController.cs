using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.IService.IEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace back_sistema_de_eventos.Controllers.Invitation

{
    //[Route("api/[controller]")] 
    //[ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public InvitationController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Endpoint para aceptar invitación con registro
        [HttpPost("AcceptWithRegistration")]
        public async Task<IActionResult> AcceptWithRegistration([FromBody] AcceptInvitationDTO request)
        {
            var @event = await _dbContext.Events.FirstOrDefaultAsync(e => e.Token == request.Token);
            if (@event == null)
            {
                return NotFound(new { message = "Evento no encontrado" });
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            var invitation = await _dbContext.Invitations
                .FirstOrDefaultAsync(i => i.IdEvent == @event.Id && i.Email == request.Email && i.IdGuest == null);
            if (invitation == null)
            {
                return NotFound(new { message = "Invitación no encontrada" });
            }

            invitation.IdGuest = user.Id;
            invitation.Status = InvitationStatus.Accepted;
            _dbContext.GuestRegistrations.Add(new GuestRegistration { IdInvitation = invitation.Id, IdUser = user.Id });
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Invitación aceptada exitosamente" });
        }

        // Endpoint para rechazar invitación
        [HttpPost("Rechazar")]
        public async Task<IActionResult> Rechazar([FromBody] RechazartInvitationDTO request)
        {
            var @event = await _dbContext.Events.FirstOrDefaultAsync(e => e.Token == request.Token);
            if (@event == null)
            {
                return NotFound(new { message = "Evento no encontrado" });
            }

            var invitation = await _dbContext.Invitations
                .FirstOrDefaultAsync(i => i.IdEvent == @event.Id && (i.IdGuest == request.UserId || i.Email == request.Email));
            if (invitation == null)
            {
                return NotFound(new { message = "Invitación no encontrada" });
            }

            invitation.Status = InvitationStatus.Cancelled;
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Invitación rechazada exitosamente" });
        }

        // Endpoint para obtener lista de invitados
        [HttpGet("GetGuests/{idEvent}")]
        public async Task<ActionResult<List<GuestDTO>>> GetGuests(int idEvent)
        {
            var invitations = await _dbContext.Invitations
                .Include(i => i.Guest)
                .Where(i => i.IdEvent == idEvent)
                .ToListAsync();

            var guests = invitations.Select(invitation => new GuestDTO
            {
                Id = invitation.Guest?.Id ?? 0,
                Name = invitation.Guest?.Name ?? "Invitado sin registrar",
                Email = invitation.Email ?? invitation.Guest?.Email,
                Status = invitation.Status.ToString(),
                IdInvitation = invitation.Id
            }).ToList();

            return Ok(guests);
        }
    }
}

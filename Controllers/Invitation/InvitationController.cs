using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Controllers.Invitation
{
    [Route("api/Invitation")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public InvitationController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Invitation/GetByEvent/{eventId}
        [HttpGet("GetByEvent/{eventId}")]
        public async Task<IActionResult> GetInvitationsByEvent(int eventId)
        {
            var invitations = await _context.Invitations
                .Include(i => i.User)
                .Where(i => i.IdEvent == eventId)
                .ToListAsync();

            if (invitations == null || !invitations.Any())
            {
                return NotFound(new { message = "No se encontraron invitaciones para este evento" });
            }
            return Ok(invitations);
        }

        // PUT: api/Invitation/UpdateStatus/{id}
        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return NotFound(new { message = "Invitación no encontrada" });
            }
            invitation.Status = dto.Status;
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
            return Ok(invitation);
        }

        // PUT: api/Invitation/RemoveUser/{id}
        [HttpPut("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUserFromInvitation(int id)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return NotFound(new { message = "Invitación no encontrada" });
            }
            invitation.IdUser = null;
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario eliminado de la invitación", invitation });
        }
    }
}



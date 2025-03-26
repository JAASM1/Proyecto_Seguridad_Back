using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_sistema_de_eventos.Models.App
{
    public class Invitation
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int IdEvent { get; set; }
<<<<<<< HEAD
        public Event Event { get; set; }
        public int? IdGuest { get; set; }
        public User Guest { get; set; }
        public string Email { get; set; } //propiedad para la lista de invitados
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        public string Token { get; set; } = string.Empty;
        public DateTime InvitedAt { get; set; } = DateTime.UtcNow;
        public GuestRegistration GuestRegistration { get; set; }
=======
        public virtual Event Event { get; set; }

        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public DateTime InvitedAt { get; set; }
>>>>>>> 9dc75588d69ef5be18b702a0bb9ed8290bbaf929
    }
}

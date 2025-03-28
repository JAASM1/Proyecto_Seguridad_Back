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
        public virtual Event Event { get; set; }

        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public DateTime InvitedAt { get; set; }
    }
}

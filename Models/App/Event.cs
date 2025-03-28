using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace back_sistema_de_eventos.Models.App
{
    public class Event
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool IsActive { get; set; } = true;
        public string Token { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(User))]
        public int IdOrganizer { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}

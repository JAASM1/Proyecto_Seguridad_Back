using System.Text.Json.Serialization;

namespace back_sistema_de_eventos.Models.App
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventDateTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int IdOrganizer { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString(); // Se movio aqui ya que asi se genera el token para la url unica del evento

        [JsonIgnore]
        public User Organizer { get; set; }
        [JsonIgnore]
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();


    }
}

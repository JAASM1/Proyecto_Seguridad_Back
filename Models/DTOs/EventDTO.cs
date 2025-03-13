namespace back_sistema_de_eventos.Models.DTOs
{
    public class EventDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventDateTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int IdOrganizer { get; set; }
    }
}

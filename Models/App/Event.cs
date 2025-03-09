namespace back_sistema_de_eventos.Models.App
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int IdOrganizer { get; set; }
        public User Organizer { get; set; }

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();


    }
}

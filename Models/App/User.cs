namespace back_sistema_de_eventos.Models.App
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Event> OrganaizedEvents { get; set; } = new List<Event>();
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    }
}

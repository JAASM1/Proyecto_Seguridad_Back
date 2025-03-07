namespace back_sistema_de_eventos.Models.App
{
    public class Invitation
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public Event Event { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public string Status { get; set; } = "Pendente";
        public DateTime InvitaAt { get; set; } = DateTime.UtcNow;
    }
}

namespace back_sistema_de_eventos.Models.App
{
    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Cancelled
    }

    public class Invitation
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public Event Event { get; set; }
        public int? IdGuest { get; set; }
        public User Guest { get; set; }
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        public string Token { get; set; } = string.Empty;
        public DateTime InvitedAt { get; set; } = DateTime.UtcNow;
        public GuestRegistration GuestRegistration { get; set; }
    }
}

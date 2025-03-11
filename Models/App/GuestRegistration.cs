namespace back_sistema_de_eventos.Models.App
{
    public enum RegistrationStatus
    {
        Pending,
        Accepted,
        Cancelled
    }

    public class GuestRegistration
    {
        public int Id { get; set; }
        public int IdInvitation { get; set; }
        public Invitation Invitation { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}

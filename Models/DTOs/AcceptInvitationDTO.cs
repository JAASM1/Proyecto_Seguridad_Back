namespace back_sistema_de_eventos.Models.DTOs
{
    public class AcceptInvitationDTO
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RechazartInvitationDTO
    {
        public string Token { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
    }

    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int IdInvitation { get; set; }
    }
}

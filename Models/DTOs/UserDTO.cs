﻿namespace back_sistema_de_eventos.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }

    }
}

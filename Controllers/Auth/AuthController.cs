using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace back_sistema_de_eventos.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDBContext dbContext, JwtService jwtService, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos de inicio de sesión inválidos" });
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(user);

            // Guardar refresh token en la base de datos
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                double.Parse(_configuration["Jwt:RefreshTokenExpireDays"]));

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                accessToken,
                refreshToken,
                user = new { id = user.Id, name = user.Name, email = user.Email }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos de registro inválidos", errors = ModelState });
            }

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "El correo ya está registrado" });
            }

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _dbContext.Users.AddAsync(newUser);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.InnerException?.Message}");
                throw;
            }
            return StatusCode(201, new
            {
                message = "Usuario registrado exitosamente",
                user = new { id = newUser.Id, name = newUser.Name, email = newUser.Email }
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO tokenDto)
        {
            string accessToken = tokenDto.AccessToken;
            string refreshToken = tokenDto.RefreshToken;

            var principal = _jwtService.GetPrincipalFromToken(accessToken);
            var userId = principal.FindFirst("Id")?.Value;

            if (userId == null)
            {
                return BadRequest(new { message = "Token inválido" });
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest(new { message = "Refresh token inválido o expirado" });
            }

            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken(user);

            user.RefreshToken = newRefreshToken;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }

        [HttpPost("logout/{idUser}")]
        public async Task<IActionResult> Logout( int idUser )
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == idUser);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Sesión cerrada exitosamente" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var token = _jwtService.GeneratePasswordResetToken(user);
            var callbackUrl = Url.Action("ResetPassword", "Auth", new { token }, Request.Scheme);

            // Enviar correo con el enlace de restablecimiento de contraseña
            // Aquí se simula el envío de correo
            _logger.LogInformation($"Enlace de restablecimiento de contraseña: {callbackUrl}");

            return Ok(new { message = "Enlace de restablecimiento de contraseña enviado" });
        }
    }
}
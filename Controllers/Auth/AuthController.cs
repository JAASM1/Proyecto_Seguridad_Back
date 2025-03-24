using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly ApplicationDBContext _DbContext;
    private readonly JwtService _jwtService;

    public AuthController(ApplicationDBContext DbContext, JwtService jwtService)
    {
        _DbContext = DbContext;
        _jwtService = jwtService; 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDTO request)
    {
        var user = await _DbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return Unauthorized("Credenciales incorrectas");

        var token = _jwtService.GenerateToken(user);
        user.Token = token;
        _DbContext.Users.Update(user);
        await _DbContext.SaveChangesAsync();
        return Ok(new { token, user });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO request)
    {

        var existingUser = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return BadRequest(new { message = "El correo ya está registrado" });

        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _DbContext.Users.AddAsync(newUser);
        await _DbContext.SaveChangesAsync();
        return Ok(new {user = new { id = newUser.Id, name = newUser.Name, email = newUser.Email } });
    }

    [HttpPut("logout/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO request)
    {
        var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = "Usuario no encontrado" });

        // Actualizar los campos del usuario
        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        // Remover el token
        user.Token = null;

        _DbContext.Users.Update(user);
        await _DbContext.SaveChangesAsync();

        return Ok(new { message = "Usuario actualizado y token eliminado", user });
    }


}
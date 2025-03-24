using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _DbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return Unauthorized("Credenciales incorrectas");

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token, user });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
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

        _DbContext.Users.Add(newUser);
        await _DbContext.SaveChangesAsync();

        // Generar token JWT
        var token = _jwtService.GenerateToken(newUser);

        return Ok(new { token, user = new { id = newUser.Id, name = newUser.Name, email = newUser.Email } });
    }



}



public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
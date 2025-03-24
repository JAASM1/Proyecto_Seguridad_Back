using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;  

namespace back_sistema_de_eventos.Controllers
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        // Rutas dinámicas protegidas (con Regex)
        private readonly List<string> _protectedRoutePatterns = new List<string>
        {
            @"^/api/Event/GetEventsByUser/\d+$",          
            @"^/api/Event/GetEventsById/\d+$",            
            @"^/api/Event/GetEventsByToken/[a-zA-Z0-9]+$" 
        };

        public Middleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Verificar si la ruta coincide con un patrón dinámico
            bool isProtected = _protectedRoutePatterns.Any(pattern =>
                Regex.IsMatch(path, pattern));

            if (isProtected)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is missing");
                    return;  
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _config["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    // Si el token es válido, continúa la ejecución
                    await _next(context);
                }
                catch (SecurityTokenExpiredException)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token expired");
                    return;  
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid Token");
                    return; 
                }
            }
            else
            {
                // Si la ruta no está protegida, continúa sin verificar
                await _next(context);
            }
        }
    }
}

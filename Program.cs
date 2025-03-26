using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Controllers;
using back_sistema_de_eventos.Middleware;
using back_sistema_de_eventos.Services;
using back_sistema_de_eventos.Services.IService.IEvents;
using back_sistema_de_eventos.Services.IService.IUser;
using back_sistema_de_eventos.Services.Service;
using back_sistema_de_eventos.Services.Service.EventS;
using back_sistema_de_eventos.Services.Service.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();

// Servicios
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticación JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<JwtService>();

// CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowVueApp",
        builder => builder
        .WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// ?? Swagger configuración con JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Sistema de Eventos",
        Version = "v1"
    });

    // Esquema de autenticación
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVueApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

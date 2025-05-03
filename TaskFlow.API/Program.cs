using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data; // Pour TaskFlowDbContext
using TaskFlow.API.Services.Interfaces;
using TaskFlow.API.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Ajout du support des contrôleurs

// Add DbContext avec SQL Server
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrement des services métiers (injection de dépendances)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskFlow API",
        Version = "v1",
        Description = "API de gestion de projets et tâches pour TaskFlow"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Entrez 'Bearer {votre_token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    });
});

// Configure le port HTTPS explicitement
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7055, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

// OpenAPI + Endpoints explorer
builder.Services.AddEndpointsApiExplorer();

// Debug / Logs
builder.Logging.AddConsole();  // Ajouter des logs console pour plus de détails

// Configuration de l'authentification JWT

var jwtSettings = builder.Configuration.GetSection("Jwt");

if (string.IsNullOrEmpty(jwtSettings["Key"]))
{
    throw new InvalidOperationException("La clé JWT n'est pas configurée.");
}

var jwtKey = jwtSettings["Key"] ?? throw new InvalidOperationException("La clé JWT n'est pas configurée.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) // Safe to call GetBytes here
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
    });
}

app.UseHttpsRedirection(); // Redirection des requêtes HTTP vers HTTPS

app.UseMiddleware<TaskFlow.API.Middleware.ExceptionMiddleware>(); // Middleware pour gérer les exceptions globalement

app.UseAuthentication(); // Authentifie la requête via le token JWT
app.UseAuthorization();  // Vérifie l’accès aux routes (via [Authorize])

app.MapControllers(); // Nécessaire pour activer les contrôleurs

app.Run();

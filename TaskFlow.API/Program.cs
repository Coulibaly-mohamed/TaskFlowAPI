using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data; // Pour TaskFlowDbContext
using TaskFlow.API.Services.Interfaces;
using TaskFlow.API.Services.Implementations;

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
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TaskFlow API",
        Version = "v1",
        Description = "API de gestion de projets et tâches pour TaskFlow"
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Nécessaire pour activer les contrôleurs

app.Run();

using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data; // Pour TaskFlowDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Ajout du support des contrôleurs

// Add DbContext avec SQL Server
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// add swagger
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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();



//debug

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

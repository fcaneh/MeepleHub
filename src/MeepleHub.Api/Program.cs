using MeepleHub.Application;
using MeepleHub.Application.Queries.Games.GetGames;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using MeepleHub.Infrastructure.Persistence.Seed;
using MeepleHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<MeepleHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly));

// 3. AutoMapper (✅ Fonctionne avec AutoMapper 12.0.0)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 4. Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();

// 5. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MeepleHub API", Version = "v1" });
});

// 6. Contrôleurs
builder.Services.AddControllers();

var app = builder.Build();

// Seed la base
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MeepleHubDbContext>();
    DbInitializer.Seed(context);
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeepleHub API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
using FluentValidation;
using MediatR;
using MeepleHub.Api.MiddleWare;
using MeepleHub.Application;
using MeepleHub.Application.Commands.Games.CreateGame;
using MeepleHub.Application.Common.Behaviors;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Application.Queries.Games.GetGames;
using MeepleHub.Application.Queries.Users.GetUsers;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.ExternalRepositories;
using MeepleHub.Infrastructure.Persistence;
using MeepleHub.Infrastructure.Persistence.Seed;
using MeepleHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<MeepleHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly));

// 3. AutoMapper 
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

// 4. Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// 5. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MeepleHub API", Version = "v1" });
});

// 6. Contrôleurs
builder.Services.AddControllers();

// 7. Services
builder.Services.AddValidatorsFromAssemblyContaining<CreateGameCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddHttpClient<IBggClient, BggClient>(client =>
{
    client.DefaultRequestHeaders.UserAgent.ParseAdd("MeepleHub/0.1");
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

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
//using MeepleHub.Application;
//using MeepleHub.Application.Queries.Games.GetGames;
//using MeepleHub.Domain.Interfaces;
//using MeepleHub.Infrastructure.Persistence;
//using MeepleHub.Infrastructure.Persistence.Seed;
//using MeepleHub.Infrastructure.Repositories;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<MeepleHubDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly));
////builder.Services.AddAutoMapper(typeof(Program));
////builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
//builder.Services.AddScoped<IGameRepository, GameRepository>();

//// Add services to the container.
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddOpenApi();
////builder.Services.AddSwaggerGen();

//try
//{

//var app = builder.Build();
//using(var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<MeepleHubDbContext>();
//    DbInitializer.Seed(context);
//}


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.MapOpenApi();
//        app.UseSwagger();
//        app.UseSwaggerUI();


//}

//app.UseHttpsRedirection();

//app.Run();
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//    throw;
//}

using MeepleHub.Application;
using MeepleHub.Application.Queries.Games.GetGames;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using MeepleHub.Infrastructure.Persistence.Seed;
using MeepleHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<MeepleHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. MediatR (pour CQRS)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly));

// 3. AutoMapper (✅ CORRIGÉ pour .NET 10)
//builder.Services.AddAutoMapper(typeof(MappingProfile)); // ✅ Sans .Assembly
//builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// 4. Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();

// 5. OpenAPI + Swagger (✅ CORRIGÉ pour .NET 10)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); // ✅ Utilise OpenAPI au lieu de SwaggerGen
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeepleHub API", Version = "v1" }); // ✅ Utilise OpenApiInfo
//});

var app = builder.Build();

// Seed la base de données
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MeepleHubDbContext>();
    DbInitializer.Seed(context);
}

// ✅ Middleware dans le bon ordre
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeepleHub API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers(); // ✅ Nécessaire pour les contrôleurs
app.Run();
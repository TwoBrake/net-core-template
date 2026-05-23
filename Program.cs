using Mapster;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Data;
using NetCoreTemplate.Routes;
using NetCoreTemplate.Services;

// Create Builder
var builder = WebApplication.CreateBuilder(args);

// Load Services
builder.Services.AddOpenApi();
builder.Services.AddScoped<IRoute, CarsRoute>();
builder.Services.AddDbContext<DatabaseContext>(options => { options.UseNpgsql(builder.Configuration.GetConnectionString("Default")); });
builder.Services.AddMapster();

var app = builder.Build();

// Dynamically Load Routes
using (var scope = app.Services.CreateScope())
{
    var routes = scope.ServiceProvider.GetServices<IRoute>();

    foreach (var route in routes)
    {
        route.MapRoutes(app);
    }
}

// Allow internal injection in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Force HTTPS if possible.
app.UseHttpsRedirection();

// Start Application
app.Run();
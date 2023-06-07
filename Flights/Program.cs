using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Add DB Context
builder.Services.AddDbContext<Entities>(options =>
    options.UseInMemoryDatabase(
        databaseName: "Flights"), ServiceLifetime.Singleton);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7098"
    });
    c.CustomOperationIds(e =>
    {
        return $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}";
    });
});

builder.Services.AddSingleton<Entities>();


var app = builder.Build();

var entities = app.Services
    .CreateScope()
    .ServiceProvider
    .GetService<Entities>();
var random = new Random();

Flight[] flightsToSeed = new Flight[]
{
    new(Guid.NewGuid(),
        "Deutsche BA",
        random.Next(90, 5000).ToString(),
        new TimePlace("Munchen", DateTime.Now.AddHours(
            random.Next(1, 10))),
        new TimePlace("Schiphol", DateTime.Now.AddHours(
            random.Next(4, 15))),
        2
    ),
    new(Guid.NewGuid(),
        "Deutscheasdas BA",
        random.Next(90, 5000).ToString(),
        new TimePlace("Munchen", DateTime.Now.AddHours(
            random.Next(1, 10))),
        new TimePlace("Schiphol", DateTime.Now.AddHours(
            random.Next(4, 15))),
        random.Next(1, 853)
    )
};
entities.Flights.AddRange(flightsToSeed);
entities.SaveChanges();


app.UseCors(b => b.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
     return $"{e.ActionDescriptor.RouteValues["action"]+ e.ActionDescriptor.RouteValues["controller"]}";
    });
});
var app = builder.Build();
app.UseCors(b => b.WithOrigins("*"));
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
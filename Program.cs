using Microsoft.EntityFrameworkCore;
using RouletteApi.Models;
using RouletteApi.Services.Implementations;
using RouletteApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

#region EntityFramework

builder.Services.AddDbContext<RouletteDbContext>(options => options.UseInMemoryDatabase("items"));

#endregion EntityFramework

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services

builder.Services.AddScoped<IUserService, UserService>();

#endregion Services

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
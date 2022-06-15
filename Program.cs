using Microsoft.EntityFrameworkCore;
using RouletteApi.Repositories;
using RouletteApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

#region EntityFramework

var connectionString = builder.Configuration.GetConnectionString("RouletteDb");
builder.Services.AddSqlite<RouletteDbContext>(connectionString);
//builder.Services.AddDbContext<RouletteDbContext>(options => options.UseInMemoryDatabase("RouletteMemoryDb"));

#endregion EntityFramework

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BetService>();

#endregion Services

#region Repositories

builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<BetRepo>();
builder.Services.AddScoped<RoundRepo>();

#endregion Repositories

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

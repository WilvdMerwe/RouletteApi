using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;

namespace RouletteApi.Models;

public class RouletteDbContext : DbContext
{
    public RouletteDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
}

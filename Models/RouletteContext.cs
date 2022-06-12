using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;

namespace RouletteApi.Models;

public class RouletteContext : DbContext
{
    public RouletteContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
